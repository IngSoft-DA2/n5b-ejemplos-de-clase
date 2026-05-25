using Cine.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private static readonly ActorDto[] Actors =
    [
        new ActorDto
        {
            Nombre = "Leonardo",
            Apellido = "DiCaprio",
            Calificacion = 4.9,
            Foto = "https://cdn-useast1.kapwing.com/static/templates/leonardo-dicaprio-cheers-meme-template-full-45246ac0.webp"
        },
        new ActorDto
        {
            Nombre = "Meryl",
            Apellido = "Streep",
            Calificacion = 5.0,
            Foto = "https://cdn.shopify.com/s/files/1/0522/1610/3083/files/MerylStreepMirandaPriestlySunglassesJimmyChooJC5030U5000_87.jpg"
        },
        new ActorDto
        {
            Nombre = "Denzel",
            Apellido = "Washington",
            Calificacion = 4.8,
            Foto = "https://miarevista.okdiario.com/wp-content/uploads/sites/3/2024/11/Denzel-Washington-portada.jpg"
        },
        new ActorDto
        {
            Nombre = "Natalie",
            Apellido = "Portman",
            Calificacion = 4.7,
            Foto = "https://cdn.britannica.com/86/255786-050-5A8D7B3A/actress-natalie-portman-attends-christian-dior-haute-couture-paris-fashion-week.jpg"
        },
        new ActorDto
        {
            Nombre = "Nicolas",
            Apellido = "Cage",
            Calificacion = 4.6,
            Foto = "https://i0.wp.com/cultfaction.com/wp-content/uploads/2016/08/nic-cage.jpg"
        }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<ActorDto>> GetAll()
    {
        return Ok(Actors);
    }

    [HttpGet("{apellido}")]
    public ActionResult<ActorDto> GetByLastName([FromRoute] string apellido)
    {
        var actor = Actors.FirstOrDefault(a => a.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
        if (actor is null)
        {
            return NotFound();
        }

        return Ok(actor);
    }
}