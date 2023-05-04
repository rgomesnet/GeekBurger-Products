using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Controllers;
using GeekBurger.Products.Model;
using GeekBurger.Products.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GeekBurger.Products.UnitTests.Controllers
{

    public class ProductsControllerUnitTests
    {
        private readonly ProductsController _productsController;
        private Mock<IProductsRepository> _productRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private readonly Fixture _fixture;

        public ProductsControllerUnitTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _productRepositoryMock = new Mock<IProductsRepository>();
            _mapperMock = new Mock<IMapper>();
            _productsController = new ProductsController(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void OnGetProductsByStoreName_WhenListIsEmpty_ShouldReturnNotFound()
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

        [Fact]
        public void OnGetProductsByStoreName_WhenListIsNotEmpty_ShouldReturnOneProduct()
        {
            //arrange
            var storeName = "Paulista";
            var productList = _fixture.Build<Product>().With(x => x.Name, storeName).CreateMany(1).ToList();

            _productRepositoryMock.Setup(_ => _.GetProductsByStoreName(It.IsAny<string>())).Returns(productList);

            var expected = new OkObjectResult(new List<ProductToGet>
            {
                new ProductToGet{
                StoreId = productList.First().StoreId,
                ProductId = productList.First().ProductId,
                Image = productList.First().Image,
                Name = productList.First().Name,
                Price = productList.First().Price
            }});

            _mapperMock.Setup(_ => _.Map<IEnumerable<ProductToGet>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(new List<ProductToGet>
                {
                    new ProductToGet {
                    StoreId = productList.First().StoreId,
                    ProductId = productList.First().ProductId,
                    Image = productList.First().Image,
                    Name = productList.First().Name,
                    Price = productList.First().Price
                } });

            //act
            var response = _productsController.GetProductsByStoreName(storeName);

            //assert            
            Assert.IsType<OkObjectResult>(response);
            response.Should().BeEquivalentTo(expected);
        }
    }
}
