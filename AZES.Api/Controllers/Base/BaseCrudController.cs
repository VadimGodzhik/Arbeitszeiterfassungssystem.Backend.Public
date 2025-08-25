using System;
using System.Collections.Generic;
using BRIT.Dal.Exceptions;
using BRIT.Models.Entities.Base;
using BRIT.Models.Entities;
using BRIT.Dal.Repos.Base;
//using BRIT.Services.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;
using BRIT.Services.ApiWrapper;
using Microsoft.Extensions.Options;
using System.Text.Json;
using NuGet.Protocol;
using AutoMapper;
using BRIT.Dal.EfStructures;
using BRIT.Models.DtoEntities.Base;

namespace AZES.Api.Controllers.Base
{
    public abstract class BaseCrudController<T, TDto, TDtoId, TController> : ControllerBase
        where T : BaseEntities, new()
        where TDto : BaseDtoEntities, new()
        where TDtoId : BaseDtoEntitiesWithId, new()
        where TController : BaseCrudController<T, TDto, TDtoId, TController>
    {

        protected readonly IRepo<T> MainRepo;
        //protected readonly IAppLogging<TController> Logger;
        //protected BaseCrudController(IRepo<T> repo, IAppLogging<TController> logger)
        protected readonly IMapper MapperDtoEntities;
        protected readonly TDto Dto;
        protected readonly TDtoId DtoId;


        protected BaseCrudController(IRepo<T> repo, IMapper mapperDtoEntities, TDto dto, TDtoId dtoId)

        {

            MapperDtoEntities = mapperDtoEntities;
            MainRepo = repo;
            Dto = dto;
            DtoId = dtoId;
            //Logger = logger;

        }
        
        //private ApiServiceSettings _settings = new ApiServiceSettings();

        // private ApiServiceSettings _settings = new ApiServiceSettings();
        //private IConfiguration _config;
        //private HttpClient _client = new HttpClient();


        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns>All records</returns>
        /// <response code="200">Returns all items</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpGet]
        public ActionResult<IEnumerable<T>> GetAll()
        {
            return Ok(MainRepo.GetAllIgnoreQueryFilters());
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
        [HttpGet("{id}")]
        public ActionResult<T> GetOne(int id)
        {
            var entity = MainRepo.Find(id);

            if (entity == null)
            {
                return NoContent();
            }

            return Ok(entity);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPut("{id}")]
        public IActionResult UpdateOne(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                MainRepo.Update(entity);
            }
            catch (CustomException ex)
            {
                //This shows an example with the custom exception
                //Should handle more gracefully
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return Ok(entity);
        }

        /// <summary>
        /// Adds a single record
        /// </summary>
        /// <remarks>
        /// Sample body:
        /// <pre>
        /// {
        ///   "Id": 1,
        ///   "TimeStamp": "AAAAAAAAB+E="
        ///   "MakeId": 1,
        ///   "Color": "Black",
        ///   "PetName": "Zippy",
        ///   "MakeColor": "VW (Black)",
        /// }
        /// </pre>
        /// </remarks>
        /// <returns>Added record</returns>
        /// <response code="201">Found and updated the record</response>
        /// <response code="400">Bad request</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(201, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<T> AddOne(T entity)
        {
            try
            {
                MainRepo.Add(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, entity);
        }



        /// <summary>
        /// Adds a single record
        /// </summary>
        /// <remarks>
        /// Sample body:
        /// <pre>
        /// {
        ///   "Id": 1,
        ///   "TimeStamp": "AAAAAAAAB+E="
        ///   "MakeId": 1,
        ///   "Color": "Black",
        ///   "PetName": "Zippy",
        ///   "MakeColor": "VW (Black)",
        /// }
        /// </pre>
        /// </remarks>
        /// <returns>Added record</returns>
        /// <response code="201">Found and updated the record</response>
        /// <response code="400">Bad request</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(201, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost("addAsync")]
        //"[(ValidateAntiForgeryToken)]"
        public async Task<ActionResult<T>> AddOneAsync(T entity)
        {
            try
            {
                await MainRepo?.AddAsync(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtAction(nameof(AddOneAsync), new { id = entity.Id }, entity);
            //return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, entity);

        }


        /*
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(201, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<T>> AddOneAsync(int id, T entity)
        {

            _settings.Uri = _config["ApiServiceSettings:Uri"];
            string entityBaseUri;
            switch (nameof(T))
            {
                case "Angestellte":
                    _settings.AngestellteBaseUri = _config["ApiServiceSettings:AngestellteBaseUri"];
                    entityBaseUri = _settings.AngestellteBaseUri;
                    break;

                case "Arbeitsandauer":
                    _settings.ArbeitsandauerBaseUri = _config["ApiServiceSettings:ArbeitsandauerBaseUri"];
                    entityBaseUri = _settings.ArbeitsandauerBaseUri;
                    break;

                case "Arbeitsort":
                    _settings.ArbeitsortBaseUri = _config["ApiServiceSettings:ArbeitsortBaseUri"];
                    entityBaseUri = _settings.ArbeitsortBaseUri;
                    break;

                case "Arbeitszeiterfassung":
                    _settings.ArbeitszeiterfassungBaseUri = _config["ApiServiceSettings:ArbeitszeiterfassungBaseUri"];
                    entityBaseUri = _settings.ArbeitszeiterfassungBaseUri;
                    break;

                case "Fundort":
                    _settings.FundortBaseUri = _config["ApiServiceSettings:FundortBaseUri"];
                    entityBaseUri = _settings.FundortBaseUri;
                    break;

                case "Kennwort":
                    _settings.KennwortBaseUri = _config["ApiServiceSettings:KennwortBaseUri"];
                    entityBaseUri = _settings.KennwortBaseUri;
                    break;

                case "Rolle":
                    _settings.RolleBaseUri = _config["ApiServiceSettings:RolleBaseUri"];
                    entityBaseUri = _settings.RolleBaseUri;
                    break;

                case "Hausanschrift":
                    _settings.HausanschriftBaseUri = _config["ApiServiceSettings:HausanschriftBaseUri"];
                    entityBaseUri = _settings.HausanschriftBaseUri;
                    break;

                case "Stadt":
                    _settings.StadtBaseUri = _config["ApiServiceSettings:StadtBaseUri"];
                    entityBaseUri = _settings.StadtBaseUri;
                    break;
                default:
                    entityBaseUri = "";
                    break;
            }

            try
            {
                MainRepo.Add(entity);
                var response = await PostAsJson($"{_settings.Uri}{entityBaseUri}",
                                JsonSerializer.Serialize(entity));
                if (response == null)
                {
                    throw new Exception("Unable to communicate with the service");
                }
                var location = response.Headers?.Location?.OriginalString;
                //return await response.Content.ReadFromJsonAsync<T>();
                return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, entity);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

           //return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, entity);

           

           
            //var updatedResponse = await _client.GetAsync(location);
            //if (updatedResponse.StatusCode == HttpStatusCode.NotFound)
            //{
            //    return null;
            //}

            
        }
        
        */




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
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [HttpDelete("{id}")]
        public ActionResult<T> DeleteOne(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            try
            {
                MainRepo.Delete(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return new BadRequestObjectResult(ex.GetBaseException()?.Message);
            }

            return Ok();
        }



        /*

        // internal Methoden. Auxiliary methods for POST, PUT, Delete
        internal async Task<HttpResponseMessage> PostAsJson(string uri, string json)
        {
            return await _client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        internal async Task<HttpResponseMessage> PutAsJson(string uri, string json)
        {
            return await _client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        internal async Task<HttpResponseMessage> DeleteAsJson(string uri, string json)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };
            return await _client.SendAsync(request);
        }

        */
    }
}
