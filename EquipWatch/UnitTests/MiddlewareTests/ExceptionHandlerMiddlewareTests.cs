using System.Net;
using Newtonsoft.Json;
using webapi.Middleware;

namespace EquipWatch.UnitTests.MiddlewareTests
{
    [TestFixture]
    public class ExceptionHandlerMiddlewareTests
    {
        [Test]
        public void GetResponse_ShouldReturnCorrectResponseForException()
        {
            // Arrange
            var middleware = new ExceptionHandlerMiddleware(context => Task.CompletedTask);

            // Test exceptions
            var exceptions = new List<Exception>
            {
                new KeyNotFoundException("Key not found"),
                new FileNotFoundException("File not found"),
                new UnauthorizedAccessException("Unauthorized access"),
                new ArgumentException("Invalid argument"),
                new InvalidOperationException("Invalid operation"),
                new Exception("Generic exception")
            };

            foreach (var exception in exceptions)
            {
                // Act
                var (code, message) = middleware.GetResponse(exception);

                // Assert
                switch (exception)
                {
                    case KeyNotFoundException:
                    case FileNotFoundException:
                        Assert.AreEqual(HttpStatusCode.NotFound, code);
                        break;
                    case UnauthorizedAccessException:
                        Assert.AreEqual(HttpStatusCode.Unauthorized, code);
                        break;
                    case ArgumentException:
                    case InvalidOperationException:
                        Assert.AreEqual(HttpStatusCode.BadRequest, code);
                        break;
                    default:
                        Assert.AreEqual(HttpStatusCode.InternalServerError, code);
                        break;
                }

                var expectedResponse = JsonConvert.SerializeObject(new SimpleResponse(exception.Message == string.Empty ? "GeneralError" : exception.Message));
                Assert.AreEqual(expectedResponse, message);
            }
        }
    }
}
