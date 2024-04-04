using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tiny_homes_project_api.Data;

//using TinyHomesProjectAPI.Data;
using TinyHomesProjectAPI.Models;

namespace TinyHomesProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        //CREATE => POST
        //READ => GET
        //UPDATE => PUT/PATCH
        //DELETE => DELETE

        //In Memory Storage for simplicity
        //private static readonly List<TodoItem> _todoItems = [];
        private readonly TodoDbContext _todoDbContext;

        public TasksController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        //Get api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            var item = _todoDbContext.TodoItems.ToList();
            return Ok(item);
        }

        //GET api/tasks/1
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        //POST api/tasks
        [HttpPost]
        public ActionResult Post([FromBody] TodoItem todoItem)
        {
            _todoDbContext.TodoItems.Add(todoItem);
            _todoDbContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }

        //PUT api/tasks/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var todoItemToUpdate = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToUpdate == null)
            {
                return NotFound();
            }

            todoItemToUpdate.Description = todoItem.Description;
            todoItemToUpdate.CreatedDate = todoItem.CreatedDate;
            todoItemToUpdate.DueDate = todoItem.DueDate;
            todoItemToUpdate.IsCompleted = todoItem.IsCompleted;
            _todoDbContext.SaveChanges();

            return NoContent();

        }

        //DELETE api/tasks/1
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var todoItemToDelete = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToDelete == null)
            {
                return NotFound();
            }
            _todoDbContext.TodoItems.Remove(todoItemToDelete);
            _todoDbContext.SaveChanges();

            return NoContent();
        }
    }
}

