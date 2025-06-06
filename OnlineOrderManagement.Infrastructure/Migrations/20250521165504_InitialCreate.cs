﻿        using System;
        using Microsoft.EntityFrameworkCore.Migrations;

        #nullable disable

        namespace OnlineOrderManagement.Infrastructure.Migrations
        {
            /// <inheritdoc />
            public partial class InitialCreate : Migration
            {
                /// <inheritdoc />
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                    migrationBuilder.CreateTable(
                        name: "Customers",
                        columns: table => new
                        {
                            Id = table.Column<int>(type: "int", nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_Customers", x => x.Id);
                        });

                    migrationBuilder.CreateTable(
                        name: "Products",
                        columns: table => new
                        {
                            Id = table.Column<int>(type: "int", nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                            StockQuantity = table.Column<int>(type: "int", nullable: false)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_Products", x => x.Id);
                        });

                    migrationBuilder.CreateTable(
                        name: "Orders",
                        columns: table => new
                        {
                            Id = table.Column<int>(type: "int", nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                            CustomerId = table.Column<int>(type: "int", nullable: false),
                            OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                            Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_Orders", x => x.Id);
                            table.ForeignKey(
                                name: "FK_Orders_Customers_CustomerId",
                                column: x => x.CustomerId,
                                principalTable: "Customers",
                                principalColumn: "Id",
                                onDelete: ReferentialAction.Cascade);
                        });

                    migrationBuilder.CreateTable(
                        name: "OrderItems",
                        columns: table => new
                        {
                            OrderId = table.Column<int>(type: "int", nullable: false),
                            ProductId = table.Column<int>(type: "int", nullable: false),
                            Quantity = table.Column<int>(type: "int", nullable: false),
                            Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                            table.ForeignKey(
                                name: "FK_OrderItems_Orders_OrderId",
                                column: x => x.OrderId,
                                principalTable: "Orders",
                                principalColumn: "Id",
                                onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                                name: "FK_OrderItems_Products_ProductId",
                                column: x => x.ProductId,
                                principalTable: "Products",
                                principalColumn: "Id",
                                onDelete: ReferentialAction.Cascade);
                        });

                    migrationBuilder.CreateTable(
                        name: "OrderStatusHistories",
                        columns: table => new
                        {
                            Id = table.Column<int>(type: "int", nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                            OrderId = table.Column<int>(type: "int", nullable: false),
                            Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                            Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                        },
                        constraints: table =>
                        {
                            table.PrimaryKey("PK_OrderStatusHistories", x => x.Id);
                            table.ForeignKey(
                                name: "FK_OrderStatusHistories_Orders_OrderId",
                                column: x => x.OrderId,
                                principalTable: "Orders",
                                principalColumn: "Id",
                                onDelete: ReferentialAction.Cascade);
                        });

                    migrationBuilder.CreateIndex(
                        name: "IX_OrderItems_ProductId",
                        table: "OrderItems",
                        column: "ProductId");

                    migrationBuilder.CreateIndex(
                        name: "IX_Orders_CustomerId",
                        table: "Orders",
                        column: "CustomerId");

                    migrationBuilder.CreateIndex(
                        name: "IX_OrderStatusHistories_OrderId",
                        table: "OrderStatusHistories",
                        column: "OrderId");
                }

                /// <inheritdoc />
                protected override void Down(MigrationBuilder migrationBuilder)
                {
                    migrationBuilder.DropTable(
                        name: "OrderItems");

                    migrationBuilder.DropTable(
                        name: "OrderStatusHistories");

                    migrationBuilder.DropTable(
                        name: "Products");

                    migrationBuilder.DropTable(
                        name: "Orders");

                    migrationBuilder.DropTable(
                        name: "Customers");
                }
            }
        }
