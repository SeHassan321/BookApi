using AutoMapper;
using BooksApi.Dto;
using BooksApi.Helpers;
using BooksApi.InterFaces;
using BooksApi.Models;
using BooksApi.Spesification;
using BooksApi.Spesification.BookSpec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roya.Errors;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {


        private readonly ApplicationDbContext _Context;
        private readonly IGenercRepositry<Book> bookRepo;
        private readonly IGenercRepositry<Image> imgRepo;
        private readonly IMapper mapper;

        public BooksController(ApplicationDbContext context, IGenercRepositry<Book> bookRepo, IMapper mapper, IGenercRepositry<Image> imgRepo)
        {
            _Context = context;
            this.bookRepo = bookRepo;
            this.mapper = mapper;
            this.imgRepo = imgRepo;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<BookViewDto>>> GetAllBoooks([FromQuery]BookParams bookParams )
        {
            var spec = new BookSpec(bookParams);
            var books = await bookRepo.GetAllDataWithSpecAsync(spec);

            var data = mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookViewDto>>(books);
            var countSpec = new BooksCountWithFilter(bookParams);
            var booksCount = await bookRepo.GetCountAsync(countSpec);


            return Ok(new Pagination<BookViewDto>(bookParams.PageIndex,bookParams.PageSize,booksCount,data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewDto>> GetBoook(int id)
        {

            var spec = new BookSpec(id);

            var book = await bookRepo.GetDataByIdWithSpecAsync(spec);
            if (book == null)
                return NotFound(new ApiErroeResponse(404));

            var data = mapper.Map<Book, BookViewDto>(book);

            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromForm] BookDto book)
        {


            if (!ModelState.IsValid) return BadRequest(new ApiErroeResponse(400, "invalid data"));
            if (book.ImagesFile == null) return BadRequest("At least Add one photo");

            try
            {
                for (int i = 0; i < book.ImagesFile.Length; i++)


                {
                    book.Images.Add(new Image());
                    DocumentSitting.addFile(book.ImagesFile[i], "images");
                    book.Images[i].Name = book.ImagesFile[i].FileName ;
                }
                var Addbook = mapper.Map<Book>(book);
                await bookRepo.Add(Addbook);
                bookRepo.SaveChange();
                return Ok("Done");

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErroeResponse(400,"something error happened"));
            }
        }

        [HttpPut("{bookid}")]
        public async Task<ActionResult> Update(int bookid, [FromForm] BookDto book)

        {


            var updatedbook = await bookRepo.GetDataByIdAsync(bookid);
            if (updatedbook == null)

                return BadRequest();

            if (book.ImagesFile != null)
            {

                var images = imgRepo.GetAllDataAsync().Result.Where(img => img.BookId == bookid);    

                foreach (var img in images)
                {
                    DocumentSitting.deleteFile("images", img.Name);
                }

                _Context.Images.RemoveRange(_Context.Images.Where(x => x.BookId == bookid));
                bookRepo.SaveChange();


                for (int i = 0; i < book.ImagesFile.Length; i++)
                {

                    updatedbook.Images.Add(new Image());
                    updatedbook.Images[i].Name = DocumentSitting.addFile(book.ImagesFile[i], "images");
                }
            }

            try
            {

                updatedbook.Title = book.Title;
                updatedbook.Overview = book.Overview;
                updatedbook.NumberOfPages = book.NumberOfPages;
                updatedbook.PublishedAt = book.PublishedAt;
                updatedbook.AutherId = book.AutherId;




                bookRepo.Update(updatedbook);
                bookRepo.SaveChange();
                return Created("done", book);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _Context.Books.Find(id);
            if (book == null)
                return NotFound($"No Book Was Found With This Id {id}");


            bookRepo.Delete(book);

            _Context.SaveChanges();

            return Ok(book);



        }


    }
}




