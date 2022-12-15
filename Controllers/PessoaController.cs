using bibliotecalogteste.Context;
using bibliotecalogteste.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace bibliotecalogteste.Controllers
{
    public class PessoaController : Controller
    {
        private readonly Contexto _db;
        private readonly ILogger<PessoaController> _logger;
        public PessoaController(Contexto db, ILogger<PessoaController> logger)
        {
            _db = db;
            _logger = logger;
        }


        // GET: PessoaController
        public async Task<IActionResult> Index()
        {
            _logger.LogTrace("Entrou na lista de usuário");
            try
            {
                var listaPessoa = await _db.Pessoas.ToListAsync();
                return View(listaPessoa);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Houve um erro ao buscar a lista de pessoas.");
                return View(new List<Pessoa>());
            }          
        }

        // GET: PessoaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException();
                }

                var pessoa = await _db.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

                if (pessoa == null)
                {
                    _logger.LogInformation("pessoa não encontrada no banco Id: {id}", id);
                    return RedirectToAction("Index");
                }
                return View(pessoa);
            }
            catch (Exception e)
            {
                _logger.LogError("Bugou aqui Details {Id}", id);
                return RedirectToAction("Index");
            }
        }

        // GET: PessoaController/Create
        public ActionResult<Pessoa> Create()
        {
            return View();
        }

        // POST: PessoaController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            try
            {
                if(pessoa.Nome == null)
                {
                    throw new Exception();
                }

                await _db.AddAsync(pessoa);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError("Bugou aqui");
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException();
                }

                var pessoa = await _db.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

                if (pessoa == null)
                {
                    _logger.LogInformation("pessoa não encontrada no banco Id: {id}", id);
                    return RedirectToAction("Index");
                }
                return View(pessoa);
            }
            catch(Exception e)
            {
                _logger.LogError("Bugou aqui EditGet {Id}", id);
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, Pessoa pessoa)
        {
            try
            {
                throw new Exception();
                var pessoaDb = await _db.Pessoas.FirstOrDefaultAsync(x => x.Id == id);
                if(pessoaDb == null)
                {
                    throw new ArgumentNullException();
                }
                pessoaDb.Nome = pessoa.Nome;
                pessoaDb.Idade = pessoa.Idade;
                _db.Update(pessoaDb);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                _logger.LogError("Bugou aqui EditPost {pessoa}", JsonConvert.SerializeObject(pessoa));
                return RedirectToAction("Index");
            }
        }

        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
