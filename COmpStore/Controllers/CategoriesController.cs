using AutoMapper;
using COmpStore.Dtos;
using COmpStore.QueryParameters;
using COmpStore.Schema.Dtos;
using COmpStore.Schema.Entities;
using COmpStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "resourcesUser")]
    public class CategoriesController:Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private ICategoryService  _categoryService;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Category>), 200)]
        public IActionResult GetAllCategory(CategoryQueryParameters categoryQueryParameters)
        {
            var allCategories = _categoryService.GetAll(categoryQueryParameters).ToList();

            var allCategoriesDto = allCategories.Select(x => Mapper.Map<CategoryDto>(x));
            Response.Headers.Add("X-Pagination",
                            JsonConvert.SerializeObject(new { totalCount = _categoryService.Count() }));

            return Ok(allCategoriesDto);
        }
        [HttpGet]
        [Route("{id}", Name = "GetSingleCategory")]
        public IActionResult GetSingleCustomer(int id)
        {
            Category categoryFromServ = _categoryService.GetSingle(id);

            if (categoryFromServ == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CategoryDto>(categoryFromServ));
        }

        // POST api/customers

        [HttpPost] 
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(typeof(CategoryDto), 400)]
        public IActionResult AddCustomer([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
            {
                return BadRequest("categorycreate object was null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category toAdd = Mapper.Map<Category>(categoryCreateDto);

            _categoryService.Add(toAdd);

            bool result = _categoryService.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception("something went wrong when adding a new category");
            }

            //return Ok(Mapper.Map<CategoryDto>(toAdd));
            return CreatedAtRoute("GetSingleCategory", new { id = toAdd.Id }, Mapper.Map<CategoryDto>(toAdd));
        }

        // PUT api/customers/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            var existingCategory = _categoryService.GetSingle(id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(updateDto, existingCategory);

            _categoryService.Update(existingCategory);

            bool result = _categoryService.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception($"something went wrong when updating the category with id: {id}");
            }

            return Ok(Mapper.Map<CategoryDto>(existingCategory));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            var existingCategory = _categoryService.GetSingle(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            _categoryService.Delete(id);

            bool result = _categoryService.Save();

            if (!result)
            {
                // return new StatusCodeResult(500);
                throw new Exception($"something went wrong when deleting the category with id: {id}");
            }

            return NoContent();
        }
    }
}
