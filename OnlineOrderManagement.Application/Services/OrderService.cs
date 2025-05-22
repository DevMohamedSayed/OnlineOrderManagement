using AutoMapper;
using OnlineOrderManagement.Domain.Entities;
using OnlineOrderManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found.");

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            foreach (var itemDto in dto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new Exception($"Product with id {itemDto.ProductId} not found.");

                if (product.StockQuantity < itemDto.Quantity)
                    throw new Exception($"Insufficient stock for product {product.Name}.");

                product.StockQuantity -= itemDto.Quantity;

                var subtotal = product.Price * itemDto.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    Subtotal = subtotal
                });

                _unitOfWork.Products.Update(product);
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return await GetOrderByIdAsync(order.Id);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }
        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return null;

            // Load navigation properties manually since generic repo might not eager-load
            // You might need to implement repository methods with Includes or use EF directly in infrastructure for complex queries
            var customer = await _unitOfWork.Customers.GetByIdAsync(order.CustomerId);

            // Map OrderItems with Product data for DTO
            var orderItems = order.OrderItems.ToList();

            var orderDto = new OrderDto(
                order.Id,
                _mapper.Map<CustomerDto>(customer),
                order.OrderDate,
                order.Status,
                orderItems.Select(oi => new OrderItemDetailDto(
                    oi.ProductId,
                    oi.Product?.Name ?? "Unknown", 
                    oi.Quantity,
                    oi.Subtotal)).ToList()
            );

            return orderDto;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
                return false;

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            order.StatusHistory.Add(new OrderStatusHistory
            {
                Status = newStatus,
                Timestamp = DateTime.UtcNow
            });

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var existing = await _unitOfWork.Orders.GetByIdAsync(id);
            if (existing == null) return false;
            await _unitOfWork.Orders.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
