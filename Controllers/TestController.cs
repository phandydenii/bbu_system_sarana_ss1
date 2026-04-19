using System.Data;
using System.Data.SqlClient;
using AspNetCore.Reporting;
using AutoMapper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Reporting.NETCore;
using LocalReport = Microsoft.Reporting.NETCore.LocalReport;


namespace BBU_SYSTEM.Controllers;

public class TestController (
    IConfiguration configuration, 
    IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    
}