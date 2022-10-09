using Microsoft.AspNetCore.Mvc;
using NTCY.Services.Table;
using NTCY.Services.Club;
using NTCY.Utils;
using AutoMapper;
using Microsoft.Extensions.Options;
using NTCY.Models.Table;
using NTCY.Models.Club;

namespace NTCY.Controllers.Table
{
    public class CardTableController : Controller
    {
        private ICardTableService _cardTableService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public CardTableController(ICardTableService cardTableService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _cardTableService = cardTableService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateCardTable()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCardTable(CardTable cardTable)
        {
            _cardTableService.Add(cardTable);
            TempData["msg"] = "<script>alert('Card Table Added Succesfully');</script>";
            return RedirectToAction("ViewCardTable", "CardTable");
        }
        [HttpGet]
        public IActionResult ViewCardTable()
        {
            try
            {
                List<CardTable> cardTables = new List<CardTable>();

                var cardTableData = _cardTableService.GetAll();
                foreach (var cardtable in cardTableData)
                {
                    var cardtableObj = _mapper.Map<CardTable>(cardtable);
                    cardTables.Add(cardtableObj);
                }
                if (cardTables != null)
                {
                    ViewBag.CardTableList = cardTables;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Card Table Details Not Available');</script>";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.innerEx = ex.InnerException.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCardTable(int tableNo)
        {
            var cardtable = _cardTableService.GetById(tableNo);
            return View(cardtable);
        }

        [HttpPost]
        public IActionResult UpdateCardTable(int cardTableNo, CardTable cardTable)
        {
            _cardTableService.Update(cardTableNo, cardTable);
            TempData["msg"] = "<script>alert('Card Table Updated Succesfully');</script>";
            return RedirectToAction("ViewCardTable", "CardTable");
        }

        public IActionResult DeleteCardTable(int cardTableNo)
        {
            _cardTableService.Delete(cardTableNo);
            TempData["msg"] = "<script>alert('Card Table Deleted Succesfully');</script>";
            return RedirectToAction("ViewCardTable", "CardTable");
        }
    }
}
