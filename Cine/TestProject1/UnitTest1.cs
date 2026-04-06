using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Cine.Controllers;
using Cine.Domain;
using Cine.Mappings;
using Moq;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    private Mock<IMovieService> _mock = null;
    private MoviesController _controller= null;

    private Movie _movie = null;

    [TestMethod]
    public void MoviesControllerTest()
    {
        _movie = new Movie()
        {
            Id = 1,
            Title = "The Dark Knight",
            Stars = 1
        };
        
        _mock = new Mock<IMovieService>(MockBehavior.Strict);
        _mock.Setup(s => s.GetById(1))
            .Returns(_movie);
        
        _controller = new MoviesController(_mock.Object);
        var _expectedMovie = MovieMapping.ToDto(_movie);
        
        
        var resul = _controller.GetById(1);
        _mock.VerifyAll();
        Assert.AreEqual(_expectedMovie.Title, _movie.Title);
        

    }

}