using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationHomework1.Models;

namespace WebApplicationHomework1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static string theme;
        private static List<string> strList;

        private static List<string> products;
        private static List<string> cart;

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
            if (strList == null)
            {
                strList = new List<string>();

            }
            if (cart == null)
            {
                cart = new List<string>();
            }
            if (products == null)
            {
                products = new List<string>
                {
                    "cheese","milk","apple"
                };
            }
        }

        /*Задание 1
           Используя Visual Studio 2017, создайте проект по шаблону
           ASP.NET Empty Web Application.
           Создайте страницу с формой для авторизации. Создайте две
           ASP.NET-темы с разными настройками стиля для этой формы.*/
        public IActionResult Auth()
        {
            ViewData["theme"] = theme;
            return View();
        }
        //Задание 1 конец


        /*Задание 2
            Используя Visual Studio 2017, создайте проект по шаблону
            ASP.NET Empty Web Application.
            Расширите проект созданный в предыдущем задании. Добавьте
            страницу настроек.
            На странице создайте два RadioButton с названиями тем, которые есть на сайте, и кнопку. При нажатии на кнопку, применяется
            выбранная тема. При заходе на страницу авторизации, элементы
            должны отображаться с учетом выбранной темы. Тему храните
            либо в cookie-нa6ope либо в сессии (на свое усмотрение).*/
        public IActionResult Index()
        {
            theme = "light";
            if (Request.Cookies["theme"] != null)
            {
                theme = Request.Cookies["theme"];
            }
            return View();
        }
        public IActionResult ChangeTheme()
        {
            return View();
        }
        public void ChangeThemeAction(string css_theme)
        {
            theme = css_theme;
            Response.Cookies.Append("theme", css_theme);
            Response.Redirect("/Home/Index");
        }
        //Задание 2 конец


        /*Задание 3
            Используя Visual Studio 2017, создайте проект по шаблону
            ASP.NET Empty Web Application.
            Используя изученные элементы управления, разработайте
            веб-форму для создания учетной записи на сайте.2
            Домашнее задание №1
            На форме должны быть пункты:
            ■ логин;
            ■ пароль;
            ■ пол;
            ■ как о нас узнали (несколько пунктов в виде checkbox);
            ■ «о себе» (многострочное поле ввода).*/
        public IActionResult Reg()
        {
            return View();
        }
        //Задание 3 конец


        /*Задание 4
            Используя Visual Studio 2017, создайте проект по шаблону
            ASP.NET Empty Web Application.
            Добавьте на страницу CheckBoxList и создайте код, который
            будет заполнять списочный контрол при загрузке. В список должны добавляться названия дней недели, при этом выходные дни в
            списке должны быть отмечены.*/
        public IActionResult DaysWeek()
        {
            List<KeyValuePair<string, bool>> week = new List<KeyValuePair<string, bool>> {
                new KeyValuePair<string, bool>("monday",false),
                new KeyValuePair<string, bool>("tuesday",false),
                new KeyValuePair<string, bool>("wednesday",false),
                new KeyValuePair<string, bool>("thursday",false),
                new KeyValuePair<string, bool>("friday",false),
                new KeyValuePair<string, bool>("saturday",true),
                new KeyValuePair<string, bool>("sunday",true),
            };
            return View(week);
        }
        //Задание 4 конец


        /*Задание 5
            Используя Visual Studio 2017, создайте проект по шаблону
            ASP.NET Empty Web Application.
            Добавьте страницу, на которой будет находиться ListBox с
            несколькими элементами. Разместите на странице поля вводов и
            кнопки для выполнения операций добавления, удаления и редактирования элементов в ListBox.*/

        public IActionResult List()
        {
            return View(strList);
        }
        public IActionResult AddToList(string textfield)
        {
            strList.Add(textfield);
            return RedirectToAction(actionName:"List");
        }
        public IActionResult DeletFromList(int id)
        {
            strList.RemoveAt(id);
            return RedirectToAction(actionName: "List");
        }
        //Задание 5 конец



        /*Задание 6
            Используя Visual Studio 2017, создайте проект по шаблону
            ASP.NET Empty Web Application.
            Разработайте страницу с двумя ListBox. Первый контрол –
            «Список продуктов», второй – «Корзина (выбранные продукты)».3
            Домашнее задание №1
            Добавьте на страницу кнопку «В корзину», – при нажатии на
            эту кнопку, выбранный элемент удаляется из списка продуктов и
            добавляется в «Корзину».
            Добавьте на страницу кнопку «Убрать из корзины». При нажатии на эту кнопку, элемент удаляется из списка «Корзина» и
            возвращается в список «Список продуктов».
            Добавьте кнопку «Перенести все в корзину» и «Убрать все
            элементы из корзины».
            ListBox должны быть настроены с режимом выбора Multiple*/
        public IActionResult Cart()
        {
            return View(cart);
        }
        public IActionResult Product()
        {
            return View(products);
        }
        public IActionResult AddToCart(int id)
        {
            cart.Add(products[id]);
            products.RemoveAt(id);
            return RedirectToAction(actionName: "Product");
        }
        public IActionResult RemoveFromCart(int id)
        {
            products.Add(cart[id]);
            cart.RemoveAt(id);
            return RedirectToAction(actionName: "Cart");
        }
        public IActionResult AddAllToCart()
        {
            cart.AddRange(products);
            products.Clear();
            return RedirectToAction(actionName: "Cart");
        }
        public IActionResult RemoveAllFromCart()
        {
            products.AddRange(cart);
            cart.Clear();
            return RedirectToAction(actionName: "Product");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}