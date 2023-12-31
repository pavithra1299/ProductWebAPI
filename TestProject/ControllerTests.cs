using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Controllers;
using Xunit;

namespace ProductWebApiTests
{
    public class ProductsControllerTests
    {

        [Fact]
        public void GetProductById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int validProductId = 1;

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(validProductId))
                          .Returns(new Product { ProductId = validProductId, ProductName = "Test Product" });

            // Act
            var result = controller.GetProductById(validProductId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.NotNull(product);
            Assert.Equal(validProductId, product.ProductId);
        }

        [Fact]
        public void AddProduct_ValidProduct_ReturnsCreatedResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            var newProduct = new Product { ProductId = 1, ProductName = "New Product" };

            // Act
            var result = controller.AddProduct(newProduct);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(createdResult.Value);
            Assert.NotNull(product);
            Assert.Equal(newProduct.ProductId, product.ProductId);
        }

        [Fact]
        public void UpdateProduct_ValidIdAndProduct_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int validProductId = 1;
            var updatedProduct = new Product { ProductId = validProductId, ProductName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(validProductId))
                          .Returns(new Product { ProductId = validProductId, ProductName = "Original Product" });

            // Act
            var result = controller.UpdateProduct(validProductId ,updatedProduct );

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.NotNull(product);
            Assert.Equal(updatedProduct.ProductId, product.ProductId);
            Assert.Equal(updatedProduct.ProductName, product.ProductName);
        }

        [Fact]
        public void DeleteProduct_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int validProductId = 1;

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(validProductId))
                          .Returns(new Product { ProductId = validProductId, ProductName = "Test Product" });

            // Act
            var result = controller.DeleteProduct(validProductId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Contains(validProductId.ToString(), message);
        }

        [Fact]
        public void GetProductById_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int invalidProductId = 999;

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.GetProductById(invalidProductId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void AddProduct_InvalidProduct_ReturnsBadRequestResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            Product invalidProduct = null;

            // Act
            var result = controller.AddProduct(invalidProduct);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int invalidProductId = 999;
            var updatedProduct = new Product { ProductId = invalidProductId, ProductName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.UpdateProduct(invalidProductId ,updatedProduct);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepository.Object);
            int invalidProductId = 999;

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.DeleteProduct(invalidProductId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}