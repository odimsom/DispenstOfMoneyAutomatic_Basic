using DispenstOfMoneyAutomatic_Basic.Application.Enums;
using DispenstOfMoneyAutomatic_Basic.Application.Services;
using DispenstOfMoneyAutomatic_Basic.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class ATMController : Controller
{
    private static DispenseMode _currentMode = DispenseMode.Efficient;
    private readonly ATMService _atmService = new ATMService();
    private const string SessionKey = "DispenseMode";

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ATMViewModel { CurrentMode = _currentMode });
    }

    [HttpPost]
    public IActionResult Index(ATMViewModel model)
    {
        if (model.AmountToWithdraw is null || model.AmountToWithdraw % 100 != 0)
        {
            model.ErrorMessage = "Solo se permiten montos múltiplos de 100.";
            model.CurrentMode = _currentMode;
            return View(model);
        }

        var result = _atmService.Dispense(model.AmountToWithdraw.Value, _currentMode);
        if (result == null)
        {
            model.ErrorMessage = "Monto incompatible con el modo actual.";
            model.CurrentMode = _currentMode;
            return View(model);
        }

        model.BillsDispensed = result;
        model.CurrentMode = _currentMode;
        return View(model);
    }

    // POST: /Dispenst/Withdraw
    [HttpPost]
    public IActionResult Withdraw(ATMViewModel model)
    {
        if (!ModelState.IsValid || model.AmountToWithdraw == null || model.AmountToWithdraw % 100 != 0)
        {
            ViewBag.Error = "Debe ingresar un monto entero y múltiplo de 100.";
            return View("Index", model);
        }

        var mode = HttpContext.Session.GetString(SessionKey);
        var selectedMode = string.IsNullOrEmpty(mode) ? DispenseMode.Efficient : Enum.Parse<DispenseMode>(mode);

        var result = _atmService.CalculateBills(model.AmountToWithdraw.Value, selectedMode);
        if (result.Count == 0)
        {
            ViewBag.Error = "El monto ingresado no puede ser dispensado con el modo seleccionado.";
            return View("Index", model);
        }

        ViewBag.Result = result;
        return View("Index", model);
    }

    [HttpGet]
    public IActionResult SetDispenseMode()
    {
        ViewBag.Mode = _currentMode;
        return View();
    }

    [HttpPost]
    public IActionResult SetDispenseMode(DispenseMode mode)
    {
        _currentMode = mode;
        return RedirectToAction("Index");
    }


}
