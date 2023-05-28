
using Business.Handlers.Storages.Commands;
using Business.Handlers.Storages.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Storages If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoragesController : BaseApiController
    {
        ///<summary>
        ///List Storages
        ///</summary>
        ///<remarks>Storages</remarks>
        ///<return>List Storages</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Storage>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetStoragesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        } 

        ///<summary>
        ///List Storages
        ///</summary>
        ///<remarks>Storages</remarks>
        ///<return>List Storages</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StorageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getdtos")]
        public async Task<IActionResult> GetDtoList()
        {
            var result = await Mediator.Send(new GetStoragesDtoQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Storages</remarks>
        ///<return>Storages List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Storage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetStorageQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add Storage.
        /// </summary>
        /// <param name="createStorage"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateStorageCommand createStorage)
        {
            var result = await Mediator.Send(createStorage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update Storage.
        /// </summary>
        /// <param name="updateStorage"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStorageCommand updateStorage)
        {
            var result = await Mediator.Send(updateStorage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete Storage.
        /// </summary>
        /// <param name="deleteStorage"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteStorageCommand deleteStorage)
        {
            var result = await Mediator.Send(deleteStorage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
