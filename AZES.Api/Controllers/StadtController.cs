using System.Collections.Generic;
using AZES.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using BRIT.Models.Entities;
using BRIT.Dal.Repos.Interfaces;
//using AutoLot.Services.Logging;
using Microsoft.AspNetCore.Http;
using BRIT.Models.DtoEntities.Base;
using BRIT.Models.DtoEntities;
using BRIT.Models.DtoEntities.DtoWhithId;
using AutoMapper;
using System.Linq;
using BRIT.Dal.EfStructures;
using AZES.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using NuGet.Protocol;
using BRIT.Dal.Exceptions;
using NuGet.Common;

namespace AZES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadtController : BaseCrudController<Stadt, StadtDto, StadtDtoWithId, StadtController>
    {

        public StadtController(IStadtRepo stadtRepo, IMapper stadtMapper, StadtDto stadtDto, StadtDtoWithId stadtDtoId) : base(stadtRepo, stadtMapper, stadtDto, stadtDtoId)
        {
        }

        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns>All records</returns>
        /// <response code="200">Returns all items</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpGet("apiMapper")]
        public ActionResult<StadtDtoWithId> GetStädte()
        {
            return Ok(MainRepo.GetAll().Select(stadt => MapperDtoEntities.Map<StadtDtoWithId>(stadt)));
        }



        /// <summary>
        /// Gets a single record
        /// </summary>
        /// <param name="id">Primary key of the record</param>
        /// <returns>Single record</returns>
        /// <response code="200">Found the record</response>
        /// <response code="204">No content</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(204, "No content")]
        [HttpGet("apiMapper/getOne/{id:int}")]
        public async Task<ActionResult<StadtDtoWithId>> GetOneStadtAsync(int id)
        {
            var stadt = await MainRepo.FindAsNoTrackingAsync(id);

            if (stadt == null)
            {
                return NoContent();
            }
            //stadt.ToString();
            return Ok(MapperDtoEntities.Map<StadtDtoWithId>(stadt));

        }






        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns>All records</returns>
        /// <response code="200">Returns all items</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost("apiMapper")]
        public async Task<ActionResult<IEnumerable<Stadt>>> AddStadt(StadtDto newStadt)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var stadt = MapperDtoEntities.Map<Stadt>(newStadt);
            await MainRepo.AddAsync(stadt);
            await MainRepo.SaveChangesAsync(cancellationToken);
            return Ok(MainRepo.GetAll().Select(stadt => MapperDtoEntities.Map<StadtDto>(stadt)));

            /*
                [HttpPost()]
                [Consumes(MediaTypeNames.Application.Json)]
                [ProducesResponseType(StatusCodes.Status201Created)]
                [ProducesResponseType(StatusCodes.Status400BadRequest)]
                public async Task<ActionResult<Product>> CreateAsync_ActionResultOfT(Product product)
                {
                    if (product.Description.Contains("XYZ Widget"))
                    {
                        return BadRequest();
                    }

                    _productContext.Products.Add(product);
                    await _productContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(CreateAsync_ActionResultOfT), new { id = product.Id }, product);
                }
             */

        }


        /// <summary>
        /// Updates a single record
        /// </summary>
        /// <remarks>
        /// Sample body:
        /// <pre>
        /// {
        ///   "Id": 1,
        ///   "DatumUrzeit" : ""
        ///   "TimeStamp": "AAAAAAAAB+E="
        ///   "Status": "BeginArbeitszeit"
        ///   "AngestellteId": 1,
        ///   "Vorname": "Jon",
        ///   "Nachname": "Flint",
        /// }
        /// </pre>
        /// </remarks>
        /// <param name="id">Primary key of the record to update</param>
        /// <returns>Single record</returns>
        /// <response code="200">Found and updated the record</response>
        /// <response code="400">Bad request</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPatch("apiMapper/UpdateOne/{id:int}")]
        public async Task<ActionResult<Stadt>> UpdateOneAsync(int id, StadtDto newStadt)
        {
            if (newStadt == null || id <= 0)
            {
                return BadRequest($"The request body is empty or this id = {id} cannot be retrieved");
            }
            CancellationToken cancellationToken = new CancellationToken();
            var stadtWhithId = new StadtDtoWithId { Id = id, Ort = newStadt.Ort, Postleitzahl = newStadt.Postleitzahl };
            var newStadtWithId = MapperDtoEntities.Map<Stadt>(stadtWhithId);
            try
            {
                var dbStadtToUpdate = await MainRepo.FindAsNoTrackingAsync(id);

                if (dbStadtToUpdate == null)
                {
                    return NotFound($"Stadt with Id = {id} not found");
                }

                if (id != dbStadtToUpdate.Id)
                {
                    return BadRequest("Stadt Id mismatch");
                }

                await MainRepo.UpdateAsync(newStadtWithId, id, cancellationToken).ConfigureAwait(false);
            }
            catch (CustomException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating database");
            }

            return Ok(MainRepo?.GetAll().Select(stadt => MapperDtoEntities.Map<StadtDtoWithId>(stadt)));

        }


        /// <summary>
        /// Deletes a single record
        /// </summary>
        /// <remarks>
        /// Sample body:
        /// <pre>
        /// {
        ///   "Id": 1,
        ///   "TimeStamp": "AAAAAAAAB+E="
        /// }
        /// </pre>
        /// </remarks>
        /// <returns>Nothing</returns>
        /// <response code="200">Found and deleted the record</response>
        /// <response code="400">Bad request</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpDelete("apiMapper/DeleteOne/{id:int}")]
        public async Task<ActionResult<Stadt>> DeleteOne(int id)
        {
            
            if (id <= 0)
            {
                return BadRequest($"This id = {id} cannot be retrieved. Id must be larger 0 (id:int > 0).");
            }
            
            try
            {
                Stadt? dbStadtToDelete = await MainRepo.FindAsNoTrackingAsync(id);
                
                if(dbStadtToDelete == null)
                {
                    return NotFound($"Stadt with Id = {id} not found");
                }

                if (id != dbStadtToDelete.Id)
                {
                    return BadRequest("Stadt Id mismatch");
                }
                CancellationToken cancellationToken = new CancellationToken();
                
                StadtDtoWithId stadtDtoWithId = MapperDtoEntities.Map<StadtDtoWithId>(dbStadtToDelete);
                Stadt stadtWithIdToDelete = MapperDtoEntities.Map<Stadt>(stadtDtoWithId);

                await MainRepo.DeleteAsync(stadtWithIdToDelete, id, cancellationToken).ConfigureAwait(false);
            }
            catch (CustomException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting database");
            }

            return Ok(MainRepo?.GetAll().Select(stadt => MapperDtoEntities.Map<StadtDtoWithId>(stadt)));

        }
    
    }

}
