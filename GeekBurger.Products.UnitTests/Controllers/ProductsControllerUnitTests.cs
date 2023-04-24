using AutoMapper;
using FluentAssertions;
using GeekBurger.Products.Controllers;
using GeekBurger.Products.Model;
using GeekBurger.Products.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Products.UnitTests.Controllers
{

    public class ProductsControllerUnitTests
    {
        private readonly ProductsController _productsController;
        private Mock<IProductsRepository> _productRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public ProductsControllerUnitTests()
        {
            _productRepositoryMock = new Mock<IProductsRepository>();
            _mapperMock = new Mock<IMapper>();
            _productsController = new ProductsController(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void OnGetProductsByStoreName()
        {
            //arrange
            var storeName = "Paulista";
            var productList = new List<Product>();
            _productRepositoryMock.Setup(_ => _.GetProductsByStoreName(storeName)).Returns(productList);
            var expected = new NotFoundObjectResult("Nenhum dado encontrado");

            //act
            var response = _productsController.GetProductsByStoreName(storeName);

            //assert            
            Assert.IsType<NotFoundObjectResult>(response);
            response.Should().BeEquivalentTo(expected);
        }
    }
}
