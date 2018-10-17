using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = GetCity(cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}")]
        public IActionResult GetPointOfInterest(int cityId, int Id)
        {
            var city = GetCity(cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == Id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreation pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if(pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid) //checks requirements on PointOfInterestForCreation
            {
                return BadRequest(ModelState);
            }

            var city = GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterests).Max(p => p.Id);

            var finalPoI = new PointOfInterest()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(finalPoI);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPoI.Id }, finalPoI);
        }

        [HttpPut("{cityId}/pointsofinterest/{poiId}")]
        public IActionResult UpdatePointOfInterest(int cityId, int poiId, [FromBody] PointOfInterestForUpdate pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
               ModelState.AddModelError("Description", "Description must be different than Name.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == poiId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{poiId}")]
        public IActionResult PartialUpdatePoint(int cityId, int poiId, [FromBody] JsonPatchDocument<PointOfInterestForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == poiId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdate()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
                
            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "Cannot have name and description be equal.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{poiId}")]
        public IActionResult DeletePoint(int cityId, int poiId)
        {
            var city = GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == poiId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterestFromStore);

            return NoContent();
        }


        public City GetCity(int cityId)
        {
            return CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        }

        


    }
}
