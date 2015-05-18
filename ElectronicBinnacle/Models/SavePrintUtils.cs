//para PDF/////
///para excel///////

//esta es del visual studio///////
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using ElectronicBinnacle.Models.Models.Samples;
using ElectronicBinnacle.Models.Models.SamplingOrder;
using Parameter = Microsoft.Office.Interop.Excel.Parameter;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using ElectronicBinnacle.Models.Context;
using System.Threading;

using Microsoft.Office.Core;
using System.Configuration;
using System.Linq;
using ElectronicBinnacle.Models.Models.UserControl;
using System.Diagnostics;

namespace ElectronicBinnacle.Models
{
    public class SavePrintUtils
    {
        internal static string SaveRegistroCampo(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\all.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //Tuple<List<List<ParamCadena>>, List<List<MuestraCadena>>> retornoCadena = setupRegistroCampo(appExl, workbook, muestra);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Registro de Campo";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //workbook.SaveAs(docPathExcel);
            //workbook.Close();
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
            //workbook = null;

            //List<List<ParamCadena>> paramHojasCadena = retornoCadena.Item1;
            //List<List<MuestraCadena>> muestrasHojasCadena = retornoCadena.Item2;

            //XLWorkbook wb = new XLWorkbook(docPathExcel);
            //IXLRange rng;

            //int hojasCadenaOffset = 1;
            //for (int paqHojaIndex = 0; paqHojaIndex < paramHojasCadena.Count; paqHojaIndex++)
            //    for (int hojaSampleIndex = 0; hojaSampleIndex < muestrasHojasCadena.Count; hojaSampleIndex++)
            //    {
            //        IXLWorksheet wsCad = wb.Worksheets.Worksheet("CADENA (" + Convert.ToString(++hojasCadenaOffset) + ")");

            //        for (int paqIndex = 0; paqIndex < paramHojasCadena[paqHojaIndex].Count; paqIndex++)
            //        {
            //            rng = wsCad.Range(6, 31 + paqIndex, 16, 31 + paqIndex);
            //            rng.Value = paramHojasCadena[paqHojaIndex][paqIndex].paramPaqName;

            //            for (int sampleIndex = 0; sampleIndex < muestrasHojasCadena[hojaSampleIndex].Count; sampleIndex++)
            //            {

            //                if (!paramHojasCadena[paqHojaIndex][paqIndex].isStandard)
            //                {
            //                    for (int paramMuestra = 0; paramMuestra < muestrasHojasCadena[hojaSampleIndex][sampleIndex].parametrosMuestraList.Count; paramMuestra++)
            //                    {
            //                        if (muestrasHojasCadena[hojaSampleIndex][sampleIndex].parametrosMuestraList[paramMuestra].ParameterId ==
            //                            paramHojasCadena[paqHojaIndex][paqIndex].paramPaqID &&
            //                            muestrasHojasCadena[hojaSampleIndex][sampleIndex].parametrosMuestraList[paramMuestra].verificacion)
            //                        {
            //                            rng = wsCad.Range(17 + sampleIndex, 31 + paqIndex, 17 + sampleIndex, 31 + paqIndex);
            //                            rng.Value = "X";
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    bool allParamChecks = true;
            //                    foreach (WorkPackage wpaq in muestra.SamplingOrder.WorkPackages)
            //                        foreach (var paquete in wpaq.Packages)
            //                        {
            //                            if (paquete.PackageId == paramHojasCadena[paqHojaIndex][paqIndex].paramPaqID)
            //                            {
            //                                foreach (Parameter param in paquete.Parameters)
            //                                    foreach (ParamVerification paramVerf in muestrasHojasCadena[hojaSampleIndex][sampleIndex].parametrosMuestraList)
            //                                        if (!paramVerf.verificacion)
            //                                            allParamChecks = false;
            //                            }
            //                        }
            //                    if (allParamChecks)
            //                    {
            //                        rng = wsCad.Range(17 + sampleIndex, 31 + paqIndex, 17 + sampleIndex, 31 + paqIndex);
            //                        rng.Value = "X";
            //                    }
            //                }

            //            }
            //        }
            //    }

            ///*IXLWorksheet wsCad = wb.Worksheets.Worksheet("CADENA");
            //var col = 0;
            //foreach (var workPaq in muestra.SamplingOrder.WorkPackages)
            //    foreach (var item in workPaq.Packages)
            //        if (item.Standard)
            //        {
            //            rng = wsCad.Range(6, 31 + col, 16, 31 + col);
            //            rng.Value = item.Identifier;
            //        }

            //foreach (var workPaq in muestra.SamplingOrder.WorkPackages)
            //    foreach (var item in workPaq.Packages)
            //        if (!item.Standard)
            //        {
            //            foreach (var parm in item.Parameters)
            //            {
            //                rng = wsCad.Range(6, 31 + col, 16, 31 + col);
            //                rng.Value = parm.Identifier;
            //                col++;
            //            }
            //        }*/
            //wb.SaveAs(docPathExcel);

            //appExl.Quit();
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(appExl);
            //appExl = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveOrdenTrabajo(SamplingOrder orden, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Orden de Trabajo.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupOrdenTrabajo(workbook, orden);

            ////////Salvar el Archivo Temporal
            //string docname = orden.Creator.UserId + " Orden de Trabajo";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //try
            //{
            //    workbook.Close();
            //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
            //    workbook = null;

            //    appExl.Quit();
            //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(appExl);
            //    appExl = null;
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //}
            //catch (Exception)
            //{
            //    workbook = null;
            //    appExl = null;
            //}

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SavePlanMuestreo(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Plan de Muestreo.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupPlanMuestreo(workbook, muestra.SamplingOrder, muestra.SamplingPlan);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Plan de Muestreo";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveMuestras(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Muestras.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            ///*MUESTRA COMPUESTA*/
            //if (muestra.ComplexSamples.Count > 0)
            //{
            //    setupMuestraCompuesta(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.ComplexSamples, false);

            //    /*borrar las hojas de muestra simple*/
            //    if (muestra.SimpleSamples.Count <= 0)
            //    {
            //        var wsMS1Del = workbook.Sheets["MUESTRA SIMPLE 1"];
            //        var wsMS2Del = workbook.Sheets["MUESTRA SIMPLE 2"];
            //        var wsMSCroqDel = workbook.Sheets["CROQUIS MUESTRA SIMPLE"];

            //        appExl.DisplayAlerts = false;
            //        wsMS2Del.Delete();
            //        wsMS1Del.Delete();
            //        wsMSCroqDel.Delete();
            //        appExl.DisplayAlerts = true;
            //    }
            //}

            //if (muestra.SimpleSamples.Count > 0)
            //{
            //    /*MUESTRA SIMPLE*/
            //    setupMuestraSimple(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.SimpleSamples, false);

            //    /*borrar las hojas de muestra compuesta*/
            //    if (muestra.ComplexSamples.Count <= 0)
            //    {
            //        var wsMC1Del = workbook.Sheets["MUESTRA COMP 1"];
            //        var wsMC2Del = workbook.Sheets["MUESTRA COMP 2"];
            //        var wsMCCroqDel = workbook.Sheets["CROQUIS MUESTRA COMP"];

            //        appExl.DisplayAlerts = false;
            //        wsMC2Del.Delete();
            //        wsMC1Del.Delete();
            //        wsMCCroqDel.Delete();
            //        appExl.DisplayAlerts = true;
            //    }
            //}

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Muestras";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveMuestraCompuesta(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Muestra Compuesta.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupMuestraCompuesta(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.ComplexSamples);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Muestra Compuesta";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveMuestraSimple(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Muestra Simple.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupMuestraSimple(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.SimpleSamples);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Muestra Simple";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveCadena(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Cadena.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            ///*CADENA*/
            //setupCadena(workbook, muestra.SamplingOrder, muestra.SampleString, muestra.ComplexSamples, muestra.SimpleSamples, false);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Cadena";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveBitacora(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Bitacora.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            ///*BITACORA 1*/
            //setupBitacora1(workbook, muestra.SamplingOrder, muestra.Binnacle);

            ///*BITACORA 2*/
            //setupBitacora2(workbook, muestra.SamplingOrder, muestra.SamplingPlan, muestra.QualityControl, muestra.Binnacle);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Bitacora";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveBitacora1(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Bitacora1.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupBitacora1(workbook, muestra.SamplingOrder, muestra.Binnacle);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Bitácora 1";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        internal static string SaveBitacora2(Sample muestra, bool isExcel)
        {
            return null;
            //Excel.Application appExl;
            //Excel.Workbook workbook;
            //appExl = new Excel.Application();
            //workbook = appExl.Workbooks.Open(@ConfigurationManager.AppSettings["FileUploadDirectory"] + "plantillas\\Bitacora2.xlsx", Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //setupBitacora2(workbook, muestra.SamplingOrder, muestra.SamplingPlan, muestra.QualityControl, muestra.Binnacle);

            ////////Salvar el Archivo Temporal
            //string docname = muestra.SamplingOrder.Creator.UserId + " Bitácora 2";
            //String docPathExcel = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".xlsx";
            //if (File.Exists(docPathExcel))
            //    try
            //    {
            //        File.Delete(docPathExcel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("No se puede crear el Libro de Excel de Datos.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //workbook.SaveAs(docPathExcel);

            //workbook.Close();
            //appExl.Quit();

            //int idproc = GetIDProcces("EXCEL");

            //if (idproc != -1)
            //{
            //    Process.GetProcessById(idproc).Kill();
            //}

            //if (isExcel)
            //    return docPathExcel;
            //else
            //{
            //    String docPathPdf = @ConfigurationManager.AppSettings["FileUploadDirectory"] + docname + ".pdf";
            //    ConvertExcelToPdf(docPathExcel, docPathPdf);
            //    return docPathPdf;
            //}
        }
        //--------------------------------------------------------------------------------------
        //private static Tuple<List<List<ParamCadena>>, List<List<MuestraCadena>>> setupRegistroCampo(Excel.Application appExl, Excel.Workbook workbook, Sample muestra)
        //{
        //    /*ORDEN DE TRABAJO*/
        //    setupOrdenTrabajo(workbook, muestra.SamplingOrder);

        //    /*PLAN DE MUESTREO*/
        //    setupPlanMuestreo(workbook, muestra.SamplingOrder, muestra.SamplingPlan);

        //    /*CADENA*/
        //    Tuple<List<List<ParamCadena>>, List<List<MuestraCadena>>> retornoCadena = setupCadena(workbook, muestra.SamplingOrder, muestra.SampleString, muestra.ComplexSamples, muestra.SimpleSamples);

        //    /*MUESTRA COMPUESTA*/
        //    if (muestra.ComplexSamples.Count > 0)
        //    {
        //        setupMuestraCompuesta(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.ComplexSamples);

        //        /*borrar las hojas de muestra simple*/
        //        if (muestra.SimpleSamples.Count <= 0)
        //        {
        //            var wsMS1Del = workbook.Sheets["MUESTRA SIMPLE 1"];
        //            var wsMS2Del = workbook.Sheets["MUESTRA SIMPLE 2"];
        //            var wsMSCroqDel = workbook.Sheets["CROQUIS MUESTRA SIMPLE"];

        //            appExl.DisplayAlerts = false;
        //            wsMS2Del.Delete();
        //            wsMS1Del.Delete();
        //            wsMSCroqDel.Delete();
        //            appExl.DisplayAlerts = true;
        //        }
        //    }

        //    if (muestra.SimpleSamples.Count > 0)
        //    {
        //        /*MUESTRA SIMPLE*/
        //        setupMuestraSimple(workbook, muestra.SamplingOrder, muestra.QualityControl, muestra.SimpleSamples);

        //        /*borrar las hojas de muestra compuesta*/
        //        if (muestra.ComplexSamples.Count <= 0)
        //        {
        //            var wsMC1Del = workbook.Sheets["MUESTRA COMP 1"];
        //            var wsMC2Del = workbook.Sheets["MUESTRA COMP 2"];
        //            var wsMCCroqDel = workbook.Sheets["CROQUIS MUESTRA COMP"];

        //            appExl.DisplayAlerts = false;
        //            wsMC2Del.Delete();
        //            wsMC1Del.Delete();
        //            wsMCCroqDel.Delete();
        //            appExl.DisplayAlerts = true;
        //        }
        //    }

        //    /*BITACORA 1*/
        //    setupBitacora1(workbook, muestra.SamplingOrder, muestra.Binnacle);

        //    /*BITACORA 2*/
        //    setupBitacora2(workbook, muestra.SamplingOrder, muestra.SamplingPlan, muestra.QualityControl, muestra.Binnacle);

        //    return retornoCadena;
        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupOrdenTrabajo(Excel.Workbook workbook, SamplingOrder orden)
        //{
        //    var ws1 = workbook.Sheets["ORDEN DE MUESTREO"];
        //    var wsOrdenParam = workbook.Sheets["PARAMETROS"];

        //    /*Datos Generales del Cliente*/
        //    ws1.Range("C2", "P2").Value = orden.ClientData.SocialReason;
        //    ws1.Range("C3", "H3").Value = orden.ClientData.StreetNo;
        //    ws1.Range("J3", "P3").Value = orden.ClientData.Colony;
        //    ws1.Range("C4", "H4").Value = orden.ClientData.DelMpio;
        //    ws1.Range("J4", "L4").Value = orden.ClientData.Edo;
        //    ws1.Range("N4", "P4").Value = orden.ClientData.CP;
        //    ws1.Range("C5", "H5").Value = orden.ClientData.RFC;

        //    if (orden.ClientData.BillReport)
        //        ws1.Range("N5").Value = "X";
        //    else
        //        ws1.Range("P5").Value = "X";

        //    /*Datos particulares del sitio de muestreo*/
        //    ws1.Range("C7", "P7").Value = orden.LocationData.Place;
        //    ws1.Range("C8", "H8").Value = orden.LocationData.StreetNo;
        //    ws1.Range("J8", "P8").Value = orden.LocationData.Colony;
        //    ws1.Range("C9", "H9").Value = orden.LocationData.DelMpio;
        //    ws1.Range("J9", "L9").Value = orden.LocationData.Edo;
        //    ws1.Range("N9", "P9").Value = orden.LocationData.CP;
        //    ws1.Range("C10", "H10").Value = orden.LocationData.Contact;
        //    ws1.Range("J10", "L10").Value = orden.LocationData.Phone;
        //    ws1.Range("N10", "P10").Value = orden.LocationData.Cellphone;
        //    ws1.Range("C11", "P11").Value = orden.LocationData.Email;

        //    /*Datos del muestreo*/
        //    ws1.Range("D13", "F13").Value = orden.SamplingData.Identifier;

        //    long fechaLong = orden.SamplingData.StartTime;
        //    if (fechaLong > 0)
        //    {
        //        long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue = new DateTime(beginTicks + fechaLong * 10000);
        //        ws1.Range("J13", "L13").Value = dateValue.Day + "/" + dateValue.Month + "/" + dateValue.Year;
        //        ws1.Range("O13", "P13").Value = dateValue.Hour + ":" + dateValue.Minute + ":" + dateValue.Second;
        //    }

        //    switch (orden.SamplingData.SamplingKind)
        //    {
        //        case SamplingType.AgP:
        //            ws1.Range("D14").Value = "X";
        //            break;
        //        case SamplingType.AgR:
        //            ws1.Range("F14").Value = "X";
        //            break;
        //        case SamplingType.AgN:
        //            ws1.Range("H14").Value = "X";
        //            break;
        //        case SamplingType.AgS:
        //            ws1.Range("J14").Value = "X";
        //            break;
        //        case SamplingType.AgEst:
        //            ws1.Range("L14").Value = "X";
        //            break;
        //        case SamplingType.AgMar:
        //            ws1.Range("N14").Value = "X";
        //            break;
        //        default:
        //            break;
        //    }

        //    /*Paquetes*/
        //    int prow = 17;
        //    foreach (var workPaq in orden.WorkPackages)
        //        foreach (var paq in workPaq.Packages)
        //            if (paq.Standard)//solo muestro los standars
        //            {
        //                ws1.Range("A" + prow, "D" + prow).Value = paq.Identifier;
        //                ws1.Range("E" + prow, "H" + prow).Value = workPaq.SamplesNumber.ToString();

        //                if (workPaq.Type == SampleKind.Simple)
        //                {
        //                    ws1.Range("I" + prow, "L" + prow).Value = "Simples";
        //                    ws1.Range("M" + prow, "P" + prow).Value = "-";
        //                }
        //                else
        //                {
        //                    ws1.Range("I" + prow, "L" + prow).Value = "Compuestas";
        //                    ws1.Range("M" + prow, "P" + prow).Value = workPaq.Period.ToString();
        //                }

        //                prow++;
        //            }

        //    /*Parametros*/
        //    int rw = 3;
        //    foreach (var workPaq in orden.WorkPackages)
        //        foreach (var item in workPaq.Packages)
        //            if (item.Standard)
        //            {
        //                Excel.Range range;
        //                range = wsOrdenParam.Range("A" + rw, "E" + rw);
        //                range.Value = item.Identifier;
        //                range.Font.Bold = true;
        //                range.Font.Color = Excel.XlRgbColor.rgbDarkBlue;
        //                rw++;
        //                foreach (var parm in item.Parameters)
        //                {
        //                    wsOrdenParam.Range("A" + rw, "E" + rw).Value = parm.Identifier;
        //                    wsOrdenParam.Range("F" + rw, "G" + rw).Value = parm.Container;
        //                    wsOrdenParam.Range("H" + rw, "I" + rw).Value = parm.Volume;
        //                    wsOrdenParam.Range("J" + rw, "N" + rw).Value = parm.Preserver;
        //                    wsOrdenParam.Range("O" + rw, "P" + rw).Value = parm.TMPA;
        //                    rw++;
        //                }
        //            }

        //    Excel.Range rng2;
        //    foreach (var workPaq in orden.WorkPackages)
        //        foreach (var item in workPaq.Packages)
        //            if (!item.Standard)
        //            {
        //                foreach (var parm in item.Parameters)
        //                {
        //                    rng2 = wsOrdenParam.Range("A" + rw, "E" + rw);
        //                    rng2.Value = parm.Identifier;
        //                    rng2.Font.Bold = true;

        //                    wsOrdenParam.Range("F" + rw, "G" + rw).Value = parm.Container;
        //                    wsOrdenParam.Range("H" + rw, "I" + rw).Value = parm.Volume;
        //                    wsOrdenParam.Range("J" + rw, "N" + rw).Value = parm.Preserver;
        //                    wsOrdenParam.Range("O" + rw, "P" + rw).Value = parm.TMPA;
        //                    rw++;
        //                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(rng2);
        //                }
        //            }

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();


        //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ws1);
        //    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wsOrdenParam);

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupPlanMuestreo(Excel.Workbook workbook, SamplingOrder orden, SamplingPlan plan)
        //{
        //    var ws2 = workbook.Sheets["PLAN DE MUESTREO"];
        //    var wsPlanCroq = workbook.Sheets["CROQUIS PLAN MUESTREO"];

        //    /*Datos Generales*/
        //    ws2.Range("C2", "P2").Value = orden.ClientData.SocialReason;////datos del parametro
        //    ws2.Range("C3", "P3").Value = orden.LocationData.Place;
        //    ws2.Range("C4", "P4").Value = orden.LocationData.Address;
        //    ws2.Range("C5", "P5").Value = orden.Sampler.FullName;

        //    long dateNumber = plan.datosGenerales.fecha;
        //    if (dateNumber > 0)
        //    {
        //        long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //        ws2.Range("C6").Value = dateValue2.Day;////datos del parametro
        //        ws2.Range("D6").Value = dateValue2.Month;////datos del parametro
        //        ws2.Range("E6").Value = dateValue2.Year;////datos del parametro
        //    }

        //    ws2.Range("M6", "P6").Value = orden.SamplingData.Identifier;

        //    /*Condiciones Particulares*/
        //    foreach (SamplingPlaceKind item in plan.tipoSitioMuestreoList)
        //    {
        //        SamplingPlaceType place = item.tipoSitio;
        //        switch (place)
        //        {
        //            case SamplingPlaceType.Llave:
        //                ws2.Range("E11").Value = "X";
        //                break;
        //            case SamplingPlaceType.Garrafon:
        //                ws2.Range("K10").Value = "X";
        //                break;
        //            case SamplingPlaceType.Registro:
        //                ws2.Range("E8").Value = "X";
        //                break;
        //            case SamplingPlaceType.Carcamo:
        //                ws2.Range("H8").Value = "X";
        //                break;
        //            case SamplingPlaceType.Tubo:
        //                ws2.Range("E9").Value = "X";
        //                break;
        //            case SamplingPlaceType.Noria:
        //                ws2.Range("H10").Value = "X";
        //                break;
        //            case SamplingPlaceType.Lotico:
        //                ws2.Range("K8").Value = "X";
        //                break;
        //            case SamplingPlaceType.Lentico:
        //                ws2.Range("K9").Value = "X";
        //                break;
        //            case SamplingPlaceType.PozoMonitoreo:
        //                ws2.Range("O9").Value = "X";
        //                break;
        //            case SamplingPlaceType.Estuario:
        //                ws2.Range("E10").Value = "X";
        //                break;
        //            case SamplingPlaceType.LagunaCostera:
        //                ws2.Range("O8").Value = "X";
        //                break;
        //            case SamplingPlaceType.Orilla:
        //                ws2.Range("H11").Value = "X";
        //                break;
        //            case SamplingPlaceType.Costafuera:
        //                ws2.Range("O10").Value = "X";
        //                break;
        //            case SamplingPlaceType.Otro:
        //                ws2.Range("I11", "J11").Value = item.otroSitio;
        //                ws2.Range("K11").Value = "X";
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    /*Características del proceso que genera la descarga y Normatividad Aplicable*/
        //    switch (plan.flujoDescarga)//datos
        //    {
        //        case DownLoadFlow.Continuo:
        //            ws2.Range("H13").Value = "X";
        //            break;
        //        case DownLoadFlow.Intermitente:
        //            ws2.Range("H14").Value = "X";
        //            break;
        //        default:
        //            break;
        //    }

        //    switch (plan.descargaDirigida)
        //    {
        //        case DirectedDownloadType.Drenaje:
        //            ws2.Range("H15").Value = "X";
        //            break;
        //        case DirectedDownloadType.CuerpoRec:
        //            ws2.Range("H16").Value = "X";
        //            break;
        //        case DirectedDownloadType.Riego:
        //            ws2.Range("H17").Value = "X";
        //            break;
        //        default:
        //            break;
        //    }

        //    if (plan.horasDiaTiempoOperacionDescarga != -5000)
        //        ws2.Range("H18").Value = plan.horasDiaTiempoOperacionDescarga;

        //    if (plan.diasSemanaTiempoOperacionDescarga != -5000)
        //        ws2.Range("H19").Value = plan.diasSemanaTiempoOperacionDescarga;

        //    foreach (ProtectionTool ptool in plan.equipoProteccionList)
        //    {
        //        switch (ptool.tipo)
        //        {
        //            case ProtectionToolsType.Casco:
        //                ws2.Range("L14").Value = "X";
        //                break;
        //            case ProtectionToolsType.Mascarilla:
        //                ws2.Range("P14").Value = "X";
        //                break;
        //            case ProtectionToolsType.Lentes:
        //                ws2.Range("L15").Value = "X";
        //                break;
        //            case ProtectionToolsType.ChalecoSalvavidas:
        //                ws2.Range("P15").Value = "X";
        //                break;
        //            case ProtectionToolsType.Overall:
        //                ws2.Range("L16").Value = "X";
        //                break;
        //            case ProtectionToolsType.Botas:
        //                ws2.Range("P16").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesCuero:
        //                ws2.Range("L17").Value = "X";
        //                break;
        //            case ProtectionToolsType.Tyvex:
        //                ws2.Range("P17").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesLatex:
        //                ws2.Range("L18").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesNitrilo:
        //                ws2.Range("L19").Value = "X";
        //                break;
        //            case ProtectionToolsType.Arnes:
        //                ws2.Range("P18").Value = "X";
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    ws2.Range("A21", "P45").Value = plan.estrategiaMuestreo;

        //    Croquis croquis;
        //    using (var context = new MyContext())
        //        croquis = context.GetCroquis(orden.Id, plan.idCroquis);
        //    if (croquis != null)
        //    {
        //        if (!String.IsNullOrEmpty(croquis.croquis))
        //        {
        //            var path = convertAndSave(croquis.croquis);
        //            wsPlanCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 17, 532, 394);//x,y,w,h
        //            if (File.Exists(path))
        //                try
        //                {
        //                    File.Delete(path);
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //        }

        //        if (croquis.longitud != -5000)
        //            wsPlanCroq.Range("G30", "J30").Value = croquis.longitud;
        //        if (croquis.latitud != -5000)
        //            wsPlanCroq.Range("M30", "P30").Value = croquis.latitud;
        //    }

        //    Employee sampler;
        //    Employee creator;
        //    using (var context = new MyContext())
        //    {
        //        sampler = context.GetEmployee(orden.Sampler.EmployeeId);
        //        creator = context.GetEmployee(orden.Creator.Employee.EmployeeId);
        //    }

        //    string firmaCreador = "";
        //    string firmaMuestreador = "";
        //    if (creator != null)
        //        firmaCreador = creator.Signature;
        //    if (sampler != null)
        //        firmaMuestreador = sampler.Signature;

        //    if (!String.IsNullOrEmpty(firmaCreador))
        //    {
        //        string path = convertAndSave(firmaCreador);
        //        wsPlanCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 308, 480, 232, 80);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }
        //    if (!String.IsNullOrEmpty(firmaMuestreador))
        //    {
        //        string path = convertAndSave(firmaMuestreador);
        //        wsPlanCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 480, 232, 80);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }
        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupMuestraCompuesta(Excel.Workbook workbook, SamplingOrder orden, QualityControl controlCalidad, List<ComplexSample> muestrasCompList,
        //    bool isForRegistro = true)
        //{
        //    var wsMC1 = workbook.Sheets["MUESTRA COMP 1"];
        //    var wsMC2 = workbook.Sheets["MUESTRA COMP 2"];
        //    var wsMCCroq = workbook.Sheets["CROQUIS MUESTRA COMP"];

        //    /*Datos Generales*/
        //    wsMC1.Range("C2", "R2").Value = orden.ClientData.SocialReason;
        //    wsMC1.Range("C3", "R3").Value = orden.LocationData.Place;
        //    wsMC1.Range("C4", "R4").Value = orden.LocationData.Address;
        //    wsMC1.Range("C5", "R5").Value = orden.Sampler.Name;

        //    switch (orden.SamplingData.SamplingKind)
        //    {
        //        case SamplingType.AgP:
        //            wsMC1.Range("I6").Value = "X";
        //            break;
        //        case SamplingType.AgR:
        //            wsMC1.Range("I7").Value = "X";
        //            break;
        //        case SamplingType.AgN:
        //            wsMC1.Range("K6").Value = "X";
        //            break;
        //        case SamplingType.AgS:
        //            wsMC1.Range("K7").Value = "X";
        //            break;
        //        case SamplingType.AgEst:
        //            wsMC1.Range("M6").Value = "X";
        //            break;
        //        case SamplingType.AgMar:
        //            wsMC1.Range("M7").Value = "X";
        //            break;
        //        default:
        //            break;
        //    }

        //    wsMC1.Range("O7", "R7").Value = orden.SamplingData.Identifier;

        //    /*Control de Calidad*/
        //    wsMC1.Range("C10", "H10").Value = orden.Sampler.User.CalibrationKit.Name;
        //    wsMC1.Range("K10", "M10").Value = orden.Sampler.User.CalibrationKit.Series;
        //    wsMC1.Range("P10", "R10").Value = orden.Sampler.User.CalibrationKit.Model;

        //    wsMC1.Range("D14", "E14").Value = controlCalidad.condElectricaDulce.marca;
        //    wsMC1.Range("D15", "E15").Value = controlCalidad.condElectricaSalina.marca;
        //    wsMC1.Range("D16", "E16").Value = controlCalidad.bufferPH4.marca;
        //    wsMC1.Range("D17", "E17").Value = controlCalidad.bufferPH7.marca;
        //    wsMC1.Range("D18", "E18").Value = controlCalidad.bufferPH10.marca;
        //    wsMC1.Range("D19", "E19").Value = controlCalidad.potencialREDOX.marca;

        //    wsMC1.Range("F14", "H14").Value = controlCalidad.condElectricaDulce.lote;
        //    wsMC1.Range("F15", "H15").Value = controlCalidad.condElectricaSalina.lote;
        //    wsMC1.Range("F16", "H16").Value = controlCalidad.bufferPH4.lote;
        //    wsMC1.Range("F17", "H17").Value = controlCalidad.bufferPH7.lote;
        //    wsMC1.Range("F18", "H18").Value = controlCalidad.bufferPH10.lote;
        //    wsMC1.Range("F19", "H19").Value = controlCalidad.potencialREDOX.lote;

        //    wsMC1.Range("I14", "J14").Value = controlCalidad.condElectricaDulce.caducidad;
        //    wsMC1.Range("I15", "J15").Value = controlCalidad.condElectricaSalina.caducidad;
        //    wsMC1.Range("I16", "J16").Value = controlCalidad.bufferPH4.caducidad;
        //    wsMC1.Range("I17", "J17").Value = controlCalidad.bufferPH7.caducidad;
        //    wsMC1.Range("I18", "J18").Value = controlCalidad.bufferPH10.caducidad;
        //    wsMC1.Range("I19", "J19").Value = controlCalidad.potencialREDOX.caducidad;

        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L14").Value = controlCalidad.condElectricaDulce.calibracionInicial.temperatura;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VB;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VB != -5000 && controlCalidad.condElectricaDulce.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VB - controlCalidad.condElectricaDulce.calibracionInicial.VM;

        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L15").Value = controlCalidad.condElectricaSalina.calibracionInicial.temperatura;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VB;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VB != -5000 && controlCalidad.condElectricaSalina.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VB - controlCalidad.condElectricaSalina.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH4.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K16").Value = controlCalidad.bufferPH4.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH4.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L16").Value = controlCalidad.bufferPH4.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH4.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M16").Value = controlCalidad.bufferPH4.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH4.calibracionInicial.VB != -5000 && controlCalidad.bufferPH4.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N16").Value = controlCalidad.bufferPH4.calibracionInicial.VB - controlCalidad.bufferPH4.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH7.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K17").Value = controlCalidad.bufferPH7.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH7.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L17").Value = controlCalidad.bufferPH7.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH7.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M17").Value = controlCalidad.bufferPH7.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH7.calibracionInicial.VB != -5000 && controlCalidad.bufferPH7.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N17").Value = controlCalidad.bufferPH7.calibracionInicial.VB - controlCalidad.bufferPH7.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH10.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K18").Value = controlCalidad.bufferPH10.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH10.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L18").Value = controlCalidad.bufferPH10.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH10.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M18").Value = controlCalidad.bufferPH10.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH10.calibracionInicial.VB != -5000 && controlCalidad.bufferPH10.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N18").Value = controlCalidad.bufferPH10.calibracionInicial.VB - controlCalidad.bufferPH10.calibracionInicial.VM;

        //    if (controlCalidad.potencialREDOX.calibracionInicial.VM != -5000)
        //        wsMC1.Range("K19").Value = controlCalidad.potencialREDOX.calibracionInicial.VM;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.temperatura != -5000)
        //        wsMC1.Range("L19").Value = controlCalidad.potencialREDOX.calibracionInicial.temperatura;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.VB != -5000)
        //        wsMC1.Range("M19").Value = controlCalidad.potencialREDOX.calibracionInicial.VB;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.VB != -5000 && controlCalidad.potencialREDOX.calibracionInicial.VM != -5000)
        //        wsMC1.Range("N19").Value = controlCalidad.potencialREDOX.calibracionInicial.VB - controlCalidad.potencialREDOX.calibracionInicial.VM;

        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P14").Value = controlCalidad.condElectricaDulce.calibracionFinal.temperatura;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VB;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VB != -5000 && controlCalidad.condElectricaDulce.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VB - controlCalidad.condElectricaDulce.calibracionFinal.VM;

        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P15").Value = controlCalidad.condElectricaSalina.calibracionFinal.temperatura;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VB;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VB != -5000 && controlCalidad.condElectricaSalina.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VB - controlCalidad.condElectricaSalina.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH4.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O16").Value = controlCalidad.bufferPH4.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH4.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P16").Value = controlCalidad.bufferPH4.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH4.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q16").Value = controlCalidad.bufferPH4.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH4.calibracionFinal.VB != -5000 && controlCalidad.bufferPH4.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R16").Value = controlCalidad.bufferPH4.calibracionFinal.VB - controlCalidad.bufferPH4.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH7.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O17").Value = controlCalidad.bufferPH7.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH7.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P17").Value = controlCalidad.bufferPH7.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH7.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q17").Value = controlCalidad.bufferPH7.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH7.calibracionFinal.VB != -5000 && controlCalidad.bufferPH7.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R17").Value = controlCalidad.bufferPH7.calibracionFinal.VB - controlCalidad.bufferPH7.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH10.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O18").Value = controlCalidad.bufferPH10.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH10.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P18").Value = controlCalidad.bufferPH10.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH10.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q18").Value = controlCalidad.bufferPH10.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH10.calibracionFinal.VB != -5000 && controlCalidad.bufferPH10.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R18").Value = controlCalidad.bufferPH10.calibracionFinal.VB - controlCalidad.bufferPH10.calibracionFinal.VM;

        //    if (controlCalidad.potencialREDOX.calibracionFinal.VM != -5000)
        //        wsMC1.Range("O19").Value = controlCalidad.potencialREDOX.calibracionFinal.VM;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.temperatura != -5000)
        //        wsMC1.Range("P19").Value = controlCalidad.potencialREDOX.calibracionFinal.temperatura;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.VB != -5000)
        //        wsMC1.Range("Q19").Value = controlCalidad.potencialREDOX.calibracionFinal.VB;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.VB != -5000 && controlCalidad.potencialREDOX.calibracionFinal.VM != -5000)
        //        wsMC1.Range("R19").Value = controlCalidad.potencialREDOX.calibracionFinal.VB - controlCalidad.potencialREDOX.calibracionFinal.VM;

        //    int sheetOffset;
        //    if (isForRegistro)
        //        sheetOffset = 10;
        //    else
        //        sheetOffset = 3;

        //    for (int i = 1; i < muestrasCompList.Count; i++)
        //    {
        //        wsMCCroq.Copy(Type.Missing, workbook.Sheets[sheetOffset + (i - 1) * 3]);
        //        wsMC2.Copy(Type.Missing, workbook.Sheets[sheetOffset + (i - 1) * 3]);
        //        wsMC1.Copy(Type.Missing, workbook.Sheets[sheetOffset + (i - 1) * 3]);
        //    }

        //    int mcIndex = 1;
        //    foreach (ComplexSample muestraComp in muestrasCompList)
        //    {
        //        var wsMC1Temp = wsMC1;
        //        var wsMC2Temp = wsMC2;
        //        var wsMCCroqTemp = wsMCCroq;
        //        if (mcIndex > 1)
        //        {
        //            wsMC1Temp = workbook.Sheets["MUESTRA COMP 1 " + "(" + mcIndex + ")"];
        //            wsMC2Temp = workbook.Sheets["MUESTRA COMP 2 " + "(" + mcIndex + ")"];
        //            wsMCCroqTemp = workbook.Sheets["CROQUIS MUESTRA COMP " + "(" + mcIndex + ")"];
        //        }

        //        long dateNumber = muestraComp.datosGeneralesMuestreo.fechaInicial;
        //        if (dateNumber > 0)
        //        {
        //            long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //            DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //            wsMC1Temp.Range("D6", "F6").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //        }

        //        dateNumber = muestraComp.datosGeneralesMuestreo.fechaFinal;
        //        if (dateNumber > 0)
        //        {
        //            long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //            DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //            wsMC1Temp.Range("D7", "F7").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //        }

        //        if (controlCalidad.correccionTemp)
        //        {
        //            wsMC1Temp.Range("D11", "F11").Value = "SI";
        //            wsMC1Temp.Range("G11", "J11").Value = "Valor de Corrección (⁰C)";
        //            if (controlCalidad.valorCorreccion != -5000)
        //                wsMC1Temp.Range("K11", "M11").Value = controlCalidad.valorCorreccion;
        //        }
        //        else
        //            wsMC1Temp.Range("D11", "F11").Value = "NO";

        //        if (muestraComp.hayMedicionFlujo)
        //            wsMC1Temp.Range("H21").Value = "X";
        //        else
        //            wsMC1Temp.Range("J21").Value = "X";

        //        switch (muestraComp.tipoMetodoMedicionFlujo)
        //        {
        //            case MeasurementFlow.SecciónVelocidad:
        //                wsMC1Temp.Range("D24").Value = "X";
        //                break;
        //            case MeasurementFlow.VolumenTiempo:
        //                wsMC1Temp.Range("D23").Value = "X";
        //                break;
        //            case MeasurementFlow.VertedorTriangular:
        //                wsMC1Temp.Range("J23").Value = "X";
        //                break;
        //            case MeasurementFlow.VertedorRectangular:
        //                wsMC1Temp.Range("J24").Value = "X";
        //                break;
        //        }

        //        wsMC1Temp.Range("E27", "R27").Value = muestraComp.muestraID;

        //        int muestIncrem = 0;
        //        int rwOfset = 30;
        //        foreach (SimpleSamplingIdentifier muestraSimpleMC in muestraComp.numeroMuestraList)
        //        {
        //            /*MUESTRA COMP 1*/
        //            wsMC1Temp.Range("A" + Convert.ToString(rwOfset + muestIncrem)).Value = muestIncrem + 1;

        //            long muestHoraL = muestraSimpleMC.hora;
        //            if (muestHoraL > 0)
        //            {
        //                long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //                DateTime dateValue = new DateTime(beginTicks + muestHoraL * 10000);
        //                wsMC1Temp.Range("B" + Convert.ToString(rwOfset + muestIncrem)).Value = dateValue.Hour + ":" + dateValue.Minute;
        //                if (muestraComp.hayMedicionFlujo)
        //                    wsMC2Temp.Range("A" + Convert.ToString(4 + muestIncrem)).Value = dateValue.Hour + ":" + dateValue.Minute;
        //            }

        //            if (muestraSimpleMC.temperatura.valor0 != -5000)
        //                wsMC1Temp.Range("D" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.temperatura.valor0;
        //            if (muestraSimpleMC.temperatura.valor1 != -5000)
        //                wsMC1Temp.Range("E" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.temperatura.valor1;
        //            if (muestraSimpleMC.temperatura.valor2 != -5000)
        //                wsMC1Temp.Range("F" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.temperatura.valor2;
        //            if (muestraSimpleMC.temperatura.valor0 != -5000 && muestraSimpleMC.temperatura.valor1 != -5000 && muestraSimpleMC.temperatura.valor2 != -5000)
        //                wsMC1Temp.Range("G" + Convert.ToString(rwOfset + muestIncrem)).Value = (muestraSimpleMC.temperatura.valor0 +
        //                                                                muestraSimpleMC.temperatura.valor1 +
        //                                                                muestraSimpleMC.temperatura.valor2) / 3;

        //            if (muestraSimpleMC.pH.valor0 != -5000)
        //                wsMC1Temp.Range("H" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.pH.valor0;
        //            if (muestraSimpleMC.pH.valor1 != -5000)
        //                wsMC1Temp.Range("I" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.pH.valor1;
        //            if (muestraSimpleMC.pH.valor2 != -5000)
        //                wsMC1Temp.Range("J" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.pH.valor2;
        //            if (muestraSimpleMC.pH.valor0 != -5000 && muestraSimpleMC.pH.valor1 != -5000 && muestraSimpleMC.pH.valor2 != -5000)
        //                wsMC1Temp.Range("K" + Convert.ToString(rwOfset + muestIncrem)).Value = (muestraSimpleMC.pH.valor0 +
        //                                                                muestraSimpleMC.pH.valor1 +
        //                                                                muestraSimpleMC.pH.valor2) / 3;

        //            if (muestraSimpleMC.conductividadElectrica.valor0 != -5000)
        //                wsMC1Temp.Range("L" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.conductividadElectrica.valor0;
        //            if (muestraSimpleMC.conductividadElectrica.valor1 != -5000)
        //                wsMC1Temp.Range("M" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.conductividadElectrica.valor1;
        //            if (muestraSimpleMC.conductividadElectrica.valor2 != -5000)
        //                wsMC1Temp.Range("N" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.conductividadElectrica.valor2;
        //            if (muestraSimpleMC.conductividadElectrica.valor0 != -5000 && muestraSimpleMC.conductividadElectrica.valor1 != -5000 &&
        //                muestraSimpleMC.conductividadElectrica.valor2 != -5000)
        //                wsMC1Temp.Range("O" + Convert.ToString(rwOfset + muestIncrem)).Value = (muestraSimpleMC.conductividadElectrica.valor0 +
        //                                                                muestraSimpleMC.conductividadElectrica.valor1 +
        //                                                                muestraSimpleMC.conductividadElectrica.valor2) / 3;

        //            if (muestraSimpleMC.O2 != -5000)
        //                wsMC1Temp.Range("P" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.O2;
        //            if (muestraSimpleMC.Cl2 != -5000)
        //                wsMC1Temp.Range("Q" + Convert.ToString(rwOfset + muestIncrem)).Value = muestraSimpleMC.Cl2;

        //            switch (muestraSimpleMC.materiaFlotante)
        //            {
        //                case true:
        //                    wsMC1Temp.Range("R" + Convert.ToString(rwOfset + muestIncrem)).Value = "Presente";
        //                    break;

        //                case false:
        //                    wsMC1Temp.Range("R" + Convert.ToString(rwOfset + muestIncrem)).Value = "Ausente";
        //                    break;

        //                default:
        //                    wsMC1Temp.Range("R" + Convert.ToString(rwOfset + muestIncrem)).Value = "Sin Definir";
        //                    break;
        //            }

        //            muestIncrem++;
        //        }

        //        wsMC1Temp.Range("A39", "C39").Value = muestraComp.muestrasControlCalidad.IDBcoDeViaje;
        //        wsMC1Temp.Range("D39", "G39").Value = muestraComp.muestrasControlCalidad.IDBcoDeCampo;
        //        wsMC1Temp.Range("H39", "J39").Value = muestraComp.muestrasControlCalidad.IDBcoDeEquipo;
        //        wsMC1Temp.Range("K39", "N39").Value = muestraComp.muestrasControlCalidad.muestrasDuplicadas;
        //        wsMC1Temp.Range("O39", "R39").Value = muestraComp.muestrasControlCalidad.IDMuestrasResguardo;

        //        wsMC1Temp.Range("A42", "R49").Value = muestraComp.observaciones;

        //        /*MUESTRA COMP 2*/
        //        if (muestraComp.hayMedicionFlujo)
        //        {
        //            int varIndIndex = 0;
        //            bool allGastoGood = true;
        //            double gastoSuma = 0;
        //            foreach (IndividualVariable indVar in muestraComp.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList)
        //            {
        //                if (indVar.gasto != -5000)
        //                {
        //                    wsMC2Temp.Range("B" + Convert.ToString(4 + varIndIndex), "C" + Convert.ToString(4 + varIndIndex)).Value = indVar.gasto;
        //                    gastoSuma += indVar.gasto;
        //                }
        //                else
        //                    allGastoGood = false;

        //                varIndIndex++;
        //            }

        //            double gastoProm = 0;
        //            if (allGastoGood)
        //            {
        //                gastoProm = gastoSuma / muestraComp.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList.Count;
        //                wsMC2Temp.Range("D4", "F9").Value = gastoProm;
        //            }

        //            if (muestraComp.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido != -5000)
        //                wsMC2Temp.Range("G4", "I9").Value = muestraComp.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido;

        //            wsMC2Temp.Range("J4", "L9").Value = muestraComp.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList.Count;

        //            if (allGastoGood && muestraComp.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido != -5000)
        //            {
        //                int iterator = 0;
        //                double volInd = 0;
        //                foreach (IndividualVariable indVar in muestraComp.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList)
        //                {
        //                    volInd = (indVar.gasto * muestraComp.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido) /
        //                        (gastoProm * muestraComp.secuenciaCalculoObtenerMuestraCompuesta.variablesIndividualesList.Count);
        //                    wsMC2Temp.Range("M" + Convert.ToString(4 + iterator), "O" + Convert.ToString(4 + iterator)).Value = volInd;

        //                    wsMC2Temp.Range("P" + Convert.ToString(4 + iterator), "R" + Convert.ToString(4 + iterator)).Value = (volInd * 100) /
        //                        muestraComp.secuenciaCalculoObtenerMuestraCompuesta.volumenTotalRequerido;

        //                    iterator++;
        //                }
        //            }
        //        }
        //        int rw = 13;
        //        using (var context = new MyContext())
        //        {
        //            foreach (ParamVerification paramVerif in muestraComp.parametrosMuestraList)
        //            {
        //                var param = context.GetParam(paramVerif.ParameterId);
        //                wsMC2Temp.Range("A" + rw, "E" + rw).Value = param.Identifier;
        //                wsMC2Temp.Range("F" + rw, "H" + rw).Value = param.Container;
        //                wsMC2Temp.Range("I" + rw, "J" + rw).Value = param.Volume;
        //                wsMC2Temp.Range("K" + rw, "M" + rw).Value = param.Preserver;
        //                wsMC2Temp.Range("N" + rw, "O" + rw).Value = param.TMPA;

        //                if (paramVerif.verificacion)
        //                    wsMC2Temp.Range("P" + rw, "R" + rw).Value = "Verificado";
        //                else
        //                    wsMC2Temp.Range("P" + rw, "R" + rw).Value = " No Verificado";
        //                rw++;
        //            }
        //        }

        //        Croquis croquisMC;
        //        using (var context = new MyContext())
        //            croquisMC = context.GetCroquis(orden.Id, muestraComp.idCroquis);
        //        if (croquisMC != null)
        //        {
        //            if (!String.IsNullOrEmpty(croquisMC.croquis))
        //            {
        //                string path = convertAndSave(croquisMC.croquis);
        //                wsMCCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 17, 522, 374);//x,y,w,h
        //                if (File.Exists(path))
        //                    try
        //                    {
        //                        File.Delete(path);
        //                    }
        //                    catch (Exception)
        //                    {
        //                        MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //            }

        //            if (croquisMC.longitud != -5000)
        //                wsMCCroqTemp.Range("G30", "J30").Value = croquisMC.longitud;
        //            if (croquisMC.latitud != -5000)
        //                wsMCCroqTemp.Range("M30", "P30").Value = croquisMC.latitud;
        //        }

        //        Employee creator, sampler;
        //        using (var context = new MyContext())
        //        {
        //            sampler = context.GetEmployee(orden.Sampler.EmployeeId);
        //            creator = context.GetEmployee(orden.Creator.Employee.EmployeeId);
        //        }

        //        string firmaCreadorMC = "";
        //        string firmaMuestreadorMC = "";
        //        if (creator != null)
        //            firmaCreadorMC = creator.Signature;
        //        if (sampler != null)
        //            firmaMuestreadorMC = sampler.Signature;

        //        if (!String.IsNullOrEmpty(firmaCreadorMC))
        //        {
        //            string path = convertAndSave(firmaCreadorMC);
        //            wsMCCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 308, 474, 232, 80);//x,y,w,h
        //            if (File.Exists(path))
        //                try
        //                {
        //                    File.Delete(path);
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //        }
        //        if (!String.IsNullOrEmpty(firmaMuestreadorMC))
        //        {
        //            string path = convertAndSave(firmaMuestreadorMC);
        //            wsMCCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 474, 232, 80);//x,y,w,h
        //            if (File.Exists(path))
        //                try
        //                {
        //                    File.Delete(path);
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //        }

        //        mcIndex++;
        //    }

        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupMuestraSimple(Excel.Workbook workbook, SamplingOrder orden, QualityControl controlCalidad, List<SimpleSample> muestrasSimplesList,
        //    bool isForRegistro = true)
        //{
        //    var wsS1 = workbook.Sheets["MUESTRA SIMPLE 1"];
        //    var wsS2 = workbook.Sheets["MUESTRA SIMPLE 2"];
        //    var wsSCroq = workbook.Sheets["CROQUIS MUESTRA SIMPLE"];

        //    /*Datos Generales*/
        //    wsS1.Range("C2", "R2").Value = orden.ClientData.SocialReason;
        //    wsS1.Range("C3", "R3").Value = orden.LocationData.Place;
        //    wsS1.Range("C4", "R4").Value = orden.LocationData.Address;
        //    wsS1.Range("C5", "R5").Value = orden.Sampler.FullName;

        //    switch (orden.SamplingData.SamplingKind)
        //    {
        //        case SamplingType.AgP:
        //            wsS1.Range("I6").Value = "X";
        //            break;
        //        case SamplingType.AgR:
        //            wsS1.Range("I7").Value = "X";
        //            break;
        //        case SamplingType.AgN:
        //            wsS1.Range("K6").Value = "X";
        //            break;
        //        case SamplingType.AgS:
        //            wsS1.Range("K7").Value = "X";
        //            break;
        //        case SamplingType.AgEst:
        //            wsS1.Range("M6").Value = "X";
        //            break;
        //        case SamplingType.AgMar:
        //            wsS1.Range("M7").Value = "X";
        //            break;
        //        default:
        //            break;
        //    }

        //    wsS1.Range("O7", "R7").Value = orden.SamplingData.Identifier;
        //    /*Control Calidad*/
        //    wsS1.Range("C10", "H10").Value = orden.Sampler.User.CalibrationKit.Name;
        //    wsS1.Range("K10", "M10").Value = orden.Sampler.User.CalibrationKit.Series;
        //    wsS1.Range("P10", "R10").Value = orden.Sampler.User.CalibrationKit.Model;

        //    if (controlCalidad.correccionTemp)
        //    {
        //        wsS1.Range("D11", "F11").Value = "SI";
        //        wsS1.Range("G11", "J11").Value = "Valor de Corrección (⁰C)";
        //        if (controlCalidad.valorCorreccion != -5000)
        //            wsS1.Range("K11", "M11").Value = controlCalidad.valorCorreccion;
        //    }
        //    else
        //        wsS1.Range("D11", "F11").Value = "NO";

        //    wsS1.Range("D14", "E14").Value = controlCalidad.condElectricaDulce.marca;
        //    wsS1.Range("D15", "E15").Value = controlCalidad.condElectricaSalina.marca;
        //    wsS1.Range("D16", "E16").Value = controlCalidad.bufferPH4.marca;
        //    wsS1.Range("D17", "E17").Value = controlCalidad.bufferPH7.marca;
        //    wsS1.Range("D18", "E18").Value = controlCalidad.bufferPH10.marca;
        //    wsS1.Range("D19", "E19").Value = controlCalidad.potencialREDOX.marca;

        //    wsS1.Range("F14", "H14").Value = controlCalidad.condElectricaDulce.lote;
        //    wsS1.Range("F15", "H15").Value = controlCalidad.condElectricaSalina.lote;
        //    wsS1.Range("F16", "H16").Value = controlCalidad.bufferPH4.lote;
        //    wsS1.Range("F17", "H17").Value = controlCalidad.bufferPH7.lote;
        //    wsS1.Range("F18", "H18").Value = controlCalidad.bufferPH10.lote;
        //    wsS1.Range("F19", "H19").Value = controlCalidad.potencialREDOX.lote;

        //    wsS1.Range("I14", "J14").Value = controlCalidad.condElectricaDulce.caducidad;
        //    wsS1.Range("I15", "J15").Value = controlCalidad.condElectricaSalina.caducidad;
        //    wsS1.Range("I16", "J16").Value = controlCalidad.bufferPH4.caducidad;
        //    wsS1.Range("I17", "J17").Value = controlCalidad.bufferPH7.caducidad;
        //    wsS1.Range("I18", "J18").Value = controlCalidad.bufferPH10.caducidad;
        //    wsS1.Range("I19", "J19").Value = controlCalidad.potencialREDOX.caducidad;

        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VM != -5000)
        //        wsS1.Range("K14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L14").Value = controlCalidad.condElectricaDulce.calibracionInicial.temperatura;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VB != -5000)
        //        wsS1.Range("M14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VB;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VB != -5000 && controlCalidad.condElectricaDulce.calibracionInicial.VM != -5000)
        //        wsS1.Range("N14").Value = controlCalidad.condElectricaDulce.calibracionInicial.VB - controlCalidad.condElectricaDulce.calibracionInicial.VM;

        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VM != -5000)
        //        wsS1.Range("K15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L15").Value = controlCalidad.condElectricaSalina.calibracionInicial.temperatura;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VB != -5000)
        //        wsS1.Range("M15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VB;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VB != -5000 && controlCalidad.condElectricaSalina.calibracionInicial.VM != -5000)
        //        wsS1.Range("N15").Value = controlCalidad.condElectricaSalina.calibracionInicial.VB - controlCalidad.condElectricaSalina.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH4.calibracionInicial.VM != -5000)
        //        wsS1.Range("K16").Value = controlCalidad.bufferPH4.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH4.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L16").Value = controlCalidad.bufferPH4.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH4.calibracionInicial.VB != -5000)
        //        wsS1.Range("M16").Value = controlCalidad.bufferPH4.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH4.calibracionInicial.VB != -5000 && controlCalidad.bufferPH4.calibracionInicial.VM != -5000)
        //        wsS1.Range("N16").Value = controlCalidad.bufferPH4.calibracionInicial.VB - controlCalidad.bufferPH4.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH7.calibracionInicial.VM != -5000)
        //        wsS1.Range("K17").Value = controlCalidad.bufferPH7.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH7.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L17").Value = controlCalidad.bufferPH7.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH7.calibracionInicial.VB != -5000)
        //        wsS1.Range("M17").Value = controlCalidad.bufferPH7.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH7.calibracionInicial.VB != -5000 && controlCalidad.bufferPH7.calibracionInicial.VM != -5000)
        //        wsS1.Range("N17").Value = controlCalidad.bufferPH7.calibracionInicial.VB - controlCalidad.bufferPH7.calibracionInicial.VM;

        //    if (controlCalidad.bufferPH10.calibracionInicial.VM != -5000)
        //        wsS1.Range("K18").Value = controlCalidad.bufferPH10.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH10.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L18").Value = controlCalidad.bufferPH10.calibracionInicial.temperatura;
        //    if (controlCalidad.bufferPH10.calibracionInicial.VB != -5000)
        //        wsS1.Range("M18").Value = controlCalidad.bufferPH10.calibracionInicial.VB;
        //    if (controlCalidad.bufferPH10.calibracionInicial.VB != -5000 && controlCalidad.bufferPH10.calibracionInicial.VM != -5000)
        //        wsS1.Range("N18").Value = controlCalidad.bufferPH10.calibracionInicial.VB - controlCalidad.bufferPH10.calibracionInicial.VM;

        //    if (controlCalidad.potencialREDOX.calibracionInicial.VM != -5000)
        //        wsS1.Range("K19").Value = controlCalidad.potencialREDOX.calibracionInicial.VM;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.temperatura != -5000)
        //        wsS1.Range("L19").Value = controlCalidad.potencialREDOX.calibracionInicial.temperatura;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.VB != -5000)
        //        wsS1.Range("M19").Value = controlCalidad.potencialREDOX.calibracionInicial.VB;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.VB != -5000 && controlCalidad.potencialREDOX.calibracionInicial.VM != -5000)
        //        wsS1.Range("N19").Value = controlCalidad.potencialREDOX.calibracionInicial.VB - controlCalidad.potencialREDOX.calibracionInicial.VM;

        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VM != -5000)
        //        wsS1.Range("O14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P14").Value = controlCalidad.condElectricaDulce.calibracionFinal.temperatura;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VB;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VB != -5000 && controlCalidad.condElectricaDulce.calibracionFinal.VM != -5000)
        //        wsS1.Range("R14").Value = controlCalidad.condElectricaDulce.calibracionFinal.VB - controlCalidad.condElectricaDulce.calibracionFinal.VM;

        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VM != -5000)
        //        wsS1.Range("O15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P15").Value = controlCalidad.condElectricaSalina.calibracionFinal.temperatura;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VB;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VB != -5000 && controlCalidad.condElectricaSalina.calibracionFinal.VM != -5000)
        //        wsS1.Range("R15").Value = controlCalidad.condElectricaSalina.calibracionFinal.VB - controlCalidad.condElectricaSalina.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH4.calibracionFinal.VM != -5000)
        //        wsS1.Range("O16").Value = controlCalidad.bufferPH4.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH4.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P16").Value = controlCalidad.bufferPH4.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH4.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q16").Value = controlCalidad.bufferPH4.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH4.calibracionFinal.VB != -5000 && controlCalidad.bufferPH4.calibracionFinal.VM != -5000)
        //        wsS1.Range("R16").Value = controlCalidad.bufferPH4.calibracionFinal.VB - controlCalidad.bufferPH4.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH7.calibracionFinal.VM != -5000)
        //        wsS1.Range("O17").Value = controlCalidad.bufferPH7.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH7.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P17").Value = controlCalidad.bufferPH7.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH7.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q17").Value = controlCalidad.bufferPH7.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH7.calibracionFinal.VB != -5000 && controlCalidad.bufferPH7.calibracionFinal.VM != -5000)
        //        wsS1.Range("R17").Value = controlCalidad.bufferPH7.calibracionFinal.VB - controlCalidad.bufferPH7.calibracionFinal.VM;

        //    if (controlCalidad.bufferPH10.calibracionFinal.VM != -5000)
        //        wsS1.Range("O18").Value = controlCalidad.bufferPH10.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH10.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P18").Value = controlCalidad.bufferPH10.calibracionFinal.temperatura;
        //    if (controlCalidad.bufferPH10.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q18").Value = controlCalidad.bufferPH10.calibracionFinal.VB;
        //    if (controlCalidad.bufferPH10.calibracionFinal.VB != -5000 && controlCalidad.bufferPH10.calibracionFinal.VM != -5000)
        //        wsS1.Range("R18").Value = controlCalidad.bufferPH10.calibracionFinal.VB - controlCalidad.bufferPH10.calibracionFinal.VM;

        //    if (controlCalidad.potencialREDOX.calibracionFinal.VM != -5000)
        //        wsS1.Range("O19").Value = controlCalidad.potencialREDOX.calibracionFinal.VM;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.temperatura != -5000)
        //        wsS1.Range("P19").Value = controlCalidad.potencialREDOX.calibracionFinal.temperatura;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.VB != -5000)
        //        wsS1.Range("Q19").Value = controlCalidad.potencialREDOX.calibracionFinal.VB;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.VB != -5000 && controlCalidad.potencialREDOX.calibracionFinal.VM != -5000)
        //        wsS1.Range("R19").Value = controlCalidad.potencialREDOX.calibracionFinal.VB - controlCalidad.potencialREDOX.calibracionFinal.VM;

        //    int hojasOffset;
        //    if (isForRegistro)
        //        hojasOffset = 7;
        //    else
        //        hojasOffset = 1;

        //    //int hojaCroqInc = 2;//La primera copia
        //    for (int i = 0; i < muestrasSimplesList.Count; i++)
        //    {
        //        if (i > 0)
        //        {
        //            wsSCroq.Copy(Type.Missing, workbook.Sheets[hojasOffset]);
        //            wsS2.Copy(Type.Missing, workbook.Sheets[hojasOffset]);
        //            wsS1.Copy(Type.Missing, workbook.Sheets[hojasOffset]);
        //            hojasOffset += 3;
        //        }

        //        foreach (SimpleSamplingIdentifier muestra in muestrasSimplesList[i].identificacionMuestraList)
        //        {
        //            wsSCroq.Copy(Type.Missing, workbook.Sheets[hojasOffset]);
        //            /*var wsSPartialsCroq = workbook.Sheets[hojasOffset];
        //            wsSPartialsCroq.Name = "CROQUIS MUESTRA SIMPLE " + muestra.muestraID;//DA ERRORRRRR pq tiene caracteres no validos para el nombre de una hoja*/
        //            //hojaCroqInc++;
        //            hojasOffset++;
        //        }
        //    }

        //    if (isForRegistro)
        //        hojasOffset = 7;
        //    else
        //        hojasOffset = 1;

        //    for (int i = 0; i < muestrasSimplesList.Count; i++)
        //    {
        //        SimpleSample hojaMuestraSimple = muestrasSimplesList[i];
        //        var wsMS1Temp = wsS1;
        //        var wsMS2Temp = wsS2;
        //        var wsMSCroqTemp = wsSCroq;
        //        if (i > 0)
        //        {
        //            wsMS1Temp = workbook.Sheets[++hojasOffset];
        //            wsMS2Temp = workbook.Sheets[++hojasOffset];
        //            wsMSCroqTemp = workbook.Sheets[++hojasOffset];

        //        }

        //        long dateNumber = hojaMuestraSimple.datosGeneralesMuestreo.fechaInicial;
        //        if (dateNumber > 0)
        //        {
        //            long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //            DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //            wsMS1Temp.Range("D6", "F6").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //        }

        //        dateNumber = hojaMuestraSimple.datosGeneralesMuestreo.fechaFinal;
        //        if (dateNumber > 0)
        //        {
        //            long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //            DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //            wsMS1Temp.Range("D7", "F7").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //        }

        //        int muestIncrem = 0;
        //        int rwOffset = 24;
        //        foreach (SimpleSamplingIdentifier muestraSimple in hojaMuestraSimple.identificacionMuestraList)
        //        {
        //            /*MUESTRA SIMPLE 1*/
        //            wsMS1Temp.Range("A" + Convert.ToString(rwOffset + muestIncrem), "B" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.muestraID;

        //            long muestHoraL = muestraSimple.hora;
        //            if (muestHoraL > 0)
        //            {
        //                long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //                DateTime dateValue = new DateTime(beginTicks + muestHoraL * 10000);
        //                wsMS1Temp.Range("C" + Convert.ToString(rwOffset + muestIncrem)).Value = dateValue.Hour + ":" + dateValue.Minute;
        //            }

        //            if (muestraSimple.temperatura.valor0 != -5000)
        //                wsMS1Temp.Range("D" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.temperatura.valor0;
        //            if (muestraSimple.temperatura.valor1 != -5000)
        //                wsMS1Temp.Range("E" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.temperatura.valor1;
        //            if (muestraSimple.temperatura.valor2 != -5000)
        //                wsMS1Temp.Range("F" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.temperatura.valor2;
        //            if (muestraSimple.temperatura.valor0 != -5000 && muestraSimple.temperatura.valor1 != -5000 && muestraSimple.temperatura.valor2 != -5000)
        //                wsMS1Temp.Range("G" + Convert.ToString(rwOffset + muestIncrem)).Value = (muestraSimple.temperatura.valor0 +
        //                                                             muestraSimple.temperatura.valor1 +
        //                                                             muestraSimple.temperatura.valor2) / 3;

        //            if (muestraSimple.pH.valor0 != -5000)
        //                wsMS1Temp.Range("H" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.pH.valor0;
        //            if (muestraSimple.pH.valor1 != -5000)
        //                wsMS1Temp.Range("I" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.pH.valor1;
        //            if (muestraSimple.pH.valor2 != -5000)
        //                wsMS1Temp.Range("J" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.pH.valor2;
        //            if (muestraSimple.pH.valor0 != -5000 && muestraSimple.pH.valor1 != -5000 && muestraSimple.pH.valor2 != -5000)
        //                wsMS1Temp.Range("K" + Convert.ToString(rwOffset + muestIncrem)).Value = (muestraSimple.pH.valor0 +
        //                                                             muestraSimple.pH.valor1 +
        //                                                             muestraSimple.pH.valor2) / 3;

        //            if (muestraSimple.conductividadElectrica.valor0 != -5000)
        //                wsMS1Temp.Range("L" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.conductividadElectrica.valor0;
        //            if (muestraSimple.conductividadElectrica.valor1 != -5000)
        //                wsMS1Temp.Range("M" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.conductividadElectrica.valor1;
        //            if (muestraSimple.conductividadElectrica.valor2 != -5000)
        //                wsMS1Temp.Range("N" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.conductividadElectrica.valor2;
        //            if (muestraSimple.conductividadElectrica.valor0 != -5000 && muestraSimple.conductividadElectrica.valor1 != -5000 &&
        //                muestraSimple.conductividadElectrica.valor2 != -5000)
        //                wsMS1Temp.Range("O" + Convert.ToString(rwOffset + muestIncrem)).Value = (muestraSimple.conductividadElectrica.valor0 +
        //                                                             muestraSimple.conductividadElectrica.valor1 +
        //                                                             muestraSimple.conductividadElectrica.valor2) / 3;

        //            if (muestraSimple.O2 != -5000)
        //                wsMS1Temp.Range("P" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.O2;
        //            if (muestraSimple.Cl2 != -5000)
        //                wsMS1Temp.Range("Q" + Convert.ToString(rwOffset + muestIncrem)).Value = muestraSimple.Cl2;

        //            switch (muestraSimple.materiaFlotante)
        //            {
        //                case true:
        //                    wsMS1Temp.Range("R" + Convert.ToString(rwOffset + muestIncrem)).Value = "Presente";
        //                    break;

        //                case false:
        //                    wsMS1Temp.Range("R" + Convert.ToString(rwOffset + muestIncrem)).Value = "Ausente";
        //                    break;

        //                default:
        //                    wsMS1Temp.Range("R" + Convert.ToString(rwOffset + muestIncrem)).Value = "Sin Definir";
        //                    break;
        //            }

        //            var wsSPartialsCroq = workbook.Sheets[++hojasOffset];
        //            //wsSPartialsCroq.Name = "CROQUIS MUESTRA SIMPLE " + Convert.ToString(muestIncrem + 1);

        //            Croquis croquisMS;
        //            using (var context = new MyContext())
        //                croquisMS = context.GetCroquis(orden.Id, muestraSimple.idCroquis);
        //            if (croquisMS != null)
        //            {
        //                if (!String.IsNullOrEmpty(croquisMS.croquis))
        //                {
        //                    string path = convertAndSave(croquisMS.croquis);
        //                    wsSPartialsCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 17, 532, 394);//x,y,w,h
        //                    if (File.Exists(path))
        //                        try
        //                        {
        //                            File.Delete(path);
        //                        }
        //                        catch (Exception)
        //                        {
        //                            MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        }
        //                }

        //                if (croquisMS.longitud != -5000)
        //                    wsSPartialsCroq.Range("G30", "J30").Value = croquisMS.longitud;
        //                if (croquisMS.latitud != -5000)
        //                    wsSPartialsCroq.Range("M30", "P30").Value = croquisMS.latitud;
        //            }

        //            Employee samplerMSPart;
        //            Employee creatorMSPart;
        //            using (var context = new MyContext())
        //            {
        //                samplerMSPart = context.GetEmployee(orden.Sampler.EmployeeId);
        //                creatorMSPart = context.GetEmployee(orden.Creator.Employee.EmployeeId);
        //            }

        //            string firmaCreadorMSPart = "";
        //            string firmaMuestreadorMSPart = "";
        //            if (creatorMSPart != null)
        //                firmaCreadorMSPart = creatorMSPart.Signature;
        //            if (samplerMSPart != null)
        //                firmaMuestreadorMSPart = samplerMSPart.Signature;

        //            if (!String.IsNullOrEmpty(firmaCreadorMSPart))
        //            {
        //                string path = convertAndSave(firmaCreadorMSPart);
        //                wsSPartialsCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 308, 480, 232, 80);//x,y,w,h
        //                if (File.Exists(path))
        //                    try
        //                    {
        //                        File.Delete(path);
        //                    }
        //                    catch (Exception)
        //                    {
        //                        MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //            }
        //            if (!String.IsNullOrEmpty(firmaMuestreadorMSPart))
        //            {
        //                string path = convertAndSave(firmaMuestreadorMSPart);
        //                wsSPartialsCroq.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 480, 232, 80);//x,y,w,h
        //                if (File.Exists(path))
        //                    try
        //                    {
        //                        File.Delete(path);
        //                    }
        //                    catch (Exception)
        //                    {
        //                        MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //            }

        //            muestIncrem++;
        //        }

        //        wsMS1Temp.Range("A42", "C42").Value = hojaMuestraSimple.muestrasControlCalidad.IDBcoDeViaje;
        //        wsMS1Temp.Range("D42", "G42").Value = hojaMuestraSimple.muestrasControlCalidad.IDBcoDeCampo;
        //        wsMS1Temp.Range("H42", "J42").Value = hojaMuestraSimple.muestrasControlCalidad.IDBcoDeEquipo;
        //        wsMS1Temp.Range("K42", "N42").Value = hojaMuestraSimple.muestrasControlCalidad.muestrasDuplicadas;
        //        wsMS1Temp.Range("O42", "R42").Value = hojaMuestraSimple.muestrasControlCalidad.IDMuestrasResguardo;

        //        wsMS1Temp.Range("A45", "R51").Value = hojaMuestraSimple.observaciones;

        //        /*MUESTRA SIMPLE 2*/
        //        if (hojaMuestraSimple.pozoAS.utiliza)
        //        {
        //            wsMS2Temp.Range("I1").Value = "X";
        //            if (hojaMuestraSimple.pozoAS.volumenTubo != -5000)
        //                wsMS2Temp.Range("A5", "D5").Value = hojaMuestraSimple.pozoAS.volumenTubo;
        //            if (hojaMuestraSimple.pozoAS.volumenFiltro != -5000)
        //                wsMS2Temp.Range("E5", "H5").Value = hojaMuestraSimple.pozoAS.volumenFiltro;
        //            if (hojaMuestraSimple.pozoAS.volumenFiltro != -5000 && hojaMuestraSimple.pozoAS.volumenTubo != -5000)
        //            {
        //                wsMS2Temp.Range("I5", "M5").Value = hojaMuestraSimple.pozoAS.volumenFiltro + hojaMuestraSimple.pozoAS.volumenTubo;
        //                wsMS2Temp.Range("N5", "R5").Value = (hojaMuestraSimple.pozoAS.volumenFiltro + hojaMuestraSimple.pozoAS.volumenTubo) * 3;
        //            }

        //        }
        //        else
        //            wsMS2Temp.Range("K1").Value = "X";

        //        int rw = 9;
        //        using (var context = new MyContext())
        //        {
        //            foreach (ParamVerification paramVerif in hojaMuestraSimple.parametrosMuestraList)
        //            {
        //                var param = context.GetParam(paramVerif.ParameterId);
        //                wsMS2Temp.Range("A" + rw, "E" + rw).Value = param.Identifier;
        //                wsMS2Temp.Range("F" + rw, "H" + rw).Value = param.Container;
        //                wsMS2Temp.Range("I" + rw, "J" + rw).Value = param.Volume;
        //                wsMS2Temp.Range("K" + rw, "M" + rw).Value = param.Preserver;
        //                wsMS2Temp.Range("N" + rw, "O" + rw).Value = param.TMPA;

        //                if (paramVerif.verificacion)
        //                    wsMS2Temp.Range("P" + rw, "R" + rw).Value = "Verificado";
        //                else
        //                    wsMS2Temp.Range("P" + rw, "R" + rw).Value = "No Verificado";
        //                rw++;
        //            }
        //        }

        //        Croquis croquisMSGen;
        //        using (var context = new MyContext())
        //            croquisMSGen = context.GetCroquis(orden.Id, hojaMuestraSimple.idCroquis);
        //        if (croquisMSGen != null)
        //        {
        //            if (!String.IsNullOrEmpty(croquisMSGen.croquis))
        //            {
        //                string path = convertAndSave(croquisMSGen.croquis);
        //                wsMSCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 17, 532, 394);//x,y,w,h
        //                if (File.Exists(path))
        //                    try
        //                    {
        //                        File.Delete(path);
        //                    }
        //                    catch (Exception)
        //                    {
        //                        MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //            }

        //            if (croquisMSGen.longitud != -5000)
        //                wsMSCroqTemp.Range("G30", "J30").Value = croquisMSGen.longitud;
        //            if (croquisMSGen.latitud != -5000)
        //                wsMSCroqTemp.Range("M30", "P30").Value = croquisMSGen.latitud;
        //        }

        //        Employee samplerMS;
        //        Employee creatorMS;
        //        using (var context = new MyContext())
        //        {
        //            samplerMS = context.GetEmployee(orden.Sampler.EmployeeId);
        //            creatorMS = context.GetEmployee(orden.Creator.Employee.EmployeeId);
        //        }

        //        string firmaCreadorMS = "";
        //        string firmaMuestreadorMS = "";
        //        if (creatorMS != null)
        //            firmaCreadorMS = creatorMS.Signature;
        //        if (samplerMS != null)
        //            firmaMuestreadorMS = samplerMS.Signature;

        //        if (!String.IsNullOrEmpty(firmaCreadorMS))
        //        {
        //            string path = convertAndSave(firmaCreadorMS);
        //            wsMSCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 308, 480, 232, 80);//x,y,w,h
        //            if (File.Exists(path))
        //                try
        //                {
        //                    File.Delete(path);
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //        }
        //        if (!String.IsNullOrEmpty(firmaMuestreadorMS))
        //        {
        //            string path = convertAndSave(firmaMuestreadorMS);
        //            wsMSCroqTemp.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 1, 480, 232, 80);//x,y,w,h
        //            if (File.Exists(path))
        //                try
        //                {
        //                    File.Delete(path);
        //                }
        //                catch (Exception)
        //                {
        //                    MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //        }
        //    }
        //}
        ////--------------------------------------------------------------------------------------
        //private static Tuple<List<List<ParamCadena>>, List<List<MuestraCadena>>> setupCadena(Excel.Workbook workbook, SamplingOrder orden, SampleString sampleString, List<ComplexSample> muestrasCompList
        //                                , List<SimpleSample> muestrasSimplesList, bool isForRegistro = true)
        //{
        //    var wsCadena = workbook.Sheets["CADENA"];
        //    wsCadena.Range("E6", "O6").Value = orden.BinnacleData.SocialReason;
        //    //wsCadena.Range("E6", "O6").Value = orden.BinnacleData.;//XXXXXXXXXXXXX<> Atencion
        //    wsCadena.Range("D8", "O8").Value = orden.BinnacleData.Address;

        //    if (orden.ClientData.BillReport)
        //    {
        //        wsCadena.Range("S6", "AD6").Value = orden.ClientData.SocialReason;
        //        //wsCadena.Range("E6", "O6").Value = orden.BinnacleData.;//XXXXXXXXXXXXX<> Atencion
        //        wsCadena.Range("S8", "AD8").Value = orden.ClientData.Address;
        //        wsCadena.Range("R11", "AD11").Value = orden.ClientData.RFC;
        //    }
        //    else
        //    {
        //        wsCadena.Range("S6", "AD6").Value = orden.BillerClient.SocialReason;
        //        //wsCadena.Range("E6", "O6").Value = orden.BinnacleData.;//XXXXXXXXXXXXX<> Atencion
        //        wsCadena.Range("S8", "AD8").Value = orden.BillerClient.Address;
        //        wsCadena.Range("R11", "AD11").Value = orden.BillerClient.RFC;
        //    }

        //    wsCadena.Range("E12", "O12").Value = orden.Sampler.FullName;

        //    wsCadena.Range("AU10", "AX10").Value = sampleString.ordenMuestreoEditable;

        //    wsCadena.Range("AE37", "AL37").Value = sampleString.entrega1.nombre;
        //    long fechaLong = sampleString.entrega1.fechaHora;
        //    if (fechaLong > 0)
        //    {
        //        long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue = new DateTime(beginTicks + fechaLong * 10000);
        //        wsCadena.Range("AE38", "AL38").Value = dateValue.Day + "/" + dateValue.Month + "/" + dateValue.Year;
        //        wsCadena.Range("AE39", "AL39").Value = dateValue.Hour + ":" + dateValue.Minute;
        //    }

        //    wsCadena.Range("AE41", "AL41").Value = sampleString.recibe1.nombre;
        //    fechaLong = sampleString.recibe1.fechaHora;
        //    if (fechaLong > 0)
        //    {
        //        long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue = new DateTime(beginTicks + fechaLong * 10000);
        //        wsCadena.Range("AE42", "AL42").Value = dateValue.Day + "/" + dateValue.Month + "/" + dateValue.Year;
        //        wsCadena.Range("AE43", "AL43").Value = dateValue.Hour + ":" + dateValue.Minute;
        //    }

        //    Employee samplerCadena;
        //    using (var context = new MyContext())
        //    {
        //        samplerCadena = context.GetEmployee(orden.Sampler.EmployeeId);
        //    }

        //    var firmaMuestreadorCad = samplerCadena.Signature;
        //    if (!String.IsNullOrEmpty(firmaMuestreadorCad))
        //    {
        //        string path = convertAndSave(firmaMuestreadorCad);
        //        wsCadena.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 233, 162, 212, 42);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }

        //    if (!String.IsNullOrEmpty(sampleString.entrega1.firma))
        //    {
        //        string path = convertAndSave(sampleString.entrega1.firma);
        //        wsCadena.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 602, 539, 180, 41);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }
        //    if (!String.IsNullOrEmpty(sampleString.recibe1.firma))
        //    {
        //        string path = convertAndSave(sampleString.recibe1.firma);
        //        wsCadena.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 602, 598, 180, 41);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }

        //    /*los paquetes y parametros*/
        //    List<ParamCadena> paramPaqNames = new List<ParamCadena>();
        //    List<int> uniquePaq = new List<int>();
        //    List<int> uniqueParams = new List<int>();
        //    foreach (var workPaq in orden.WorkPackages)
        //        foreach (var paquete in workPaq.Packages)
        //            if (paquete.Standard && !uniquePaq.Contains(paquete.PackageId))
        //            {
        //                ParamCadena temp = new ParamCadena();
        //                temp.paramPaqName = paquete.Identifier;
        //                temp.paramPaqID = paquete.PackageId;
        //                temp.isStandard = true;
        //                paramPaqNames.Add(temp);
        //                uniquePaq.Add(paquete.PackageId);
        //            }
        //            else
        //            {
        //                foreach (var parm in paquete.Parameters)
        //                {
        //                    if (!uniqueParams.Contains(parm.ParameterId))
        //                    {
        //                        ParamCadena temp = new ParamCadena();
        //                        temp.paramPaqName = parm.Identifier;
        //                        temp.paramPaqID = parm.ParameterId;
        //                        temp.isStandard = false;
        //                        paramPaqNames.Add(temp);
        //                        uniqueParams.Add(parm.ParameterId);
        //                    }
        //                }
        //            }

        //    int cantColumnasParaParams = 16;
        //    int cantHojasPorPaquetes = paramPaqNames.Count / cantColumnasParaParams;
        //    if (paramPaqNames.Count % cantColumnasParaParams != 0)
        //        cantHojasPorPaquetes++;

        //    List<List<ParamCadena>> paquetesPorHojas = new List<List<ParamCadena>>();
        //    for (int i = 0; i < cantHojasPorPaquetes; i++)
        //    {
        //        List<ParamCadena> paramPaqNamesTemp = new List<ParamCadena>();
        //        for (int j = i * cantColumnasParaParams; j < paramPaqNames.Count; j++)
        //        {
        //            if (j < cantColumnasParaParams)
        //                paramPaqNamesTemp.Add(paramPaqNames[j]);
        //            else
        //                break;
        //        }
        //        paquetesPorHojas.Add(paramPaqNamesTemp);
        //    }

        //    /*las muestras*/
        //    List<MuestraCadena> allSimpleSamplingsIdent = new List<MuestraCadena>();
        //    foreach (SimpleSample hojaCampoMS in muestrasSimplesList)
        //        foreach (SimpleSamplingIdentifier muestraSimple in hojaCampoMS.identificacionMuestraList)
        //        {
        //            MuestraCadena allSimpleSamplingsIdentTemp = new MuestraCadena();
        //            allSimpleSamplingsIdentTemp.simpleSamplingIdentifier = muestraSimple;
        //            allSimpleSamplingsIdentTemp.fechaMostrar = hojaCampoMS.datosGeneralesMuestreo.fechaInicial;
        //            allSimpleSamplingsIdentTemp.parametrosMuestraList = hojaCampoMS.parametrosMuestraList;
        //            allSimpleSamplingsIdent.Add(allSimpleSamplingsIdentTemp);
        //        }
        //    foreach (ComplexSample muestraComp in muestrasCompList)
        //    {
        //        foreach (SimpleSamplingIdentifier muestraSimpleMC in muestraComp.numeroMuestraList)
        //        {
        //            MuestraCadena allSimpleSamplingsIdentTemp = new MuestraCadena();
        //            allSimpleSamplingsIdentTemp.simpleSamplingIdentifier = muestraSimpleMC;
        //            allSimpleSamplingsIdentTemp.fechaMostrar = muestraComp.datosGeneralesMuestreo.fechaInicial;
        //            allSimpleSamplingsIdentTemp.parametrosMuestraList = muestraComp.parametrosMuestraList;
        //            allSimpleSamplingsIdent.Add(allSimpleSamplingsIdentTemp);
        //        }
        //        MuestraCadena allSimpleSamplingsIdentTemp2 = new MuestraCadena();
        //        SimpleSamplingIdentifier muestraSimpleMCTemp = new SimpleSamplingIdentifier();
        //        muestraSimpleMCTemp.muestraID = muestraComp.muestraID;
        //        allSimpleSamplingsIdentTemp2.simpleSamplingIdentifier = muestraSimpleMCTemp;
        //        allSimpleSamplingsIdentTemp2.fechaMostrar = muestraComp.datosGeneralesMuestreo.fechaFinal;//Coger aparte de la fecha la hora de aqui
        //        allSimpleSamplingsIdentTemp2.parametrosMuestraList = new List<ParamVerification>();
        //        allSimpleSamplingsIdentTemp2.isMCInventada = true;
        //        allSimpleSamplingsIdent.Add(allSimpleSamplingsIdentTemp2);
        //    }


        //    int cantFilasParaMuestras = 19;
        //    int cantHojasPorMuestras = allSimpleSamplingsIdent.Count / cantFilasParaMuestras;
        //    if (allSimpleSamplingsIdent.Count % cantFilasParaMuestras != 0)
        //        cantHojasPorMuestras++;

        //    List<List<MuestraCadena>> muestrasPorHojas = new List<List<MuestraCadena>>();
        //    for (int i = 0; i < cantHojasPorMuestras; i++)
        //    {
        //        List<MuestraCadena> muestraTemp = new List<MuestraCadena>();
        //        for (int j = i * cantFilasParaMuestras; j < allSimpleSamplingsIdent.Count; j++)
        //        {
        //            if (j < cantFilasParaMuestras + i * cantFilasParaMuestras)
        //                muestraTemp.Add(allSimpleSamplingsIdent[j]);
        //            else
        //                break;
        //        }
        //        muestrasPorHojas.Add(muestraTemp);
        //    }

        //    int hojasCadenaOffset;
        //    if (isForRegistro)
        //        hojasCadenaOffset = 11;
        //    else
        //        hojasCadenaOffset = 1;

        //    int muestraIndex = 1;
        //    for (int paqIndex = 0; paqIndex < paquetesPorHojas.Count; paqIndex++)
        //        for (int hojaSampleIndex = 0; hojaSampleIndex < muestrasPorHojas.Count; hojaSampleIndex++)
        //        {
        //            wsCadena.Copy(Type.Missing, workbook.Sheets[hojasCadenaOffset]);
        //            var wsCadenaTemp = workbook.Sheets[++hojasCadenaOffset];
        //            //wsCadenaTemp.Name = "CADENA CONT. (" + Convert.ToString(muestraIndex) + ")";
        //            //hojasCadenaOffset++;

        //            muestraIndex++;

        //            for (int sampleIndex = 0; sampleIndex < muestrasPorHojas[hojaSampleIndex].Count; sampleIndex++)
        //            {
        //                wsCadenaTemp.Range("A" + Convert.ToString(17 + sampleIndex), "O" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.muestraID;

        //                long dateNumber = muestrasPorHojas[hojaSampleIndex][sampleIndex].fechaMostrar;
        //                if (dateNumber > 0)
        //                {
        //                    long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //                    DateTime dateValue2 = new DateTime(beginTicks2 + dateNumber * 10000);
        //                    wsCadenaTemp.Range("P" + Convert.ToString(17 + sampleIndex), "R" + Convert.ToString(17 + sampleIndex)).Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //                }

        //                switch (orden.SamplingData.SamplingKind)
        //                {
        //                    case SamplingType.AgP:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgP";
        //                        break;
        //                    case SamplingType.AgR:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgR";
        //                        break;
        //                    case SamplingType.AgN:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgN";
        //                        break;
        //                    case SamplingType.AgS:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgS";
        //                        break;
        //                    case SamplingType.AgEst:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgEst";
        //                        break;
        //                    case SamplingType.AgMar:
        //                        wsCadenaTemp.Range("V" + Convert.ToString(17 + sampleIndex), "X" + Convert.ToString(17 + sampleIndex)).Value = "AgMar";
        //                        break;
        //                    default:
        //                        break;
        //                }

        //                long horaLong;
        //                if (muestrasPorHojas[hojaSampleIndex][sampleIndex].isMCInventada)
        //                    horaLong = muestrasPorHojas[hojaSampleIndex][sampleIndex].fechaMostrar;
        //                else
        //                {
        //                    horaLong = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.hora;

        //                    if (muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.V != -5000)
        //                        wsCadenaTemp.Range("AU" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.V;
        //                    if (muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.P != -5000)
        //                        wsCadenaTemp.Range("AV" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.P;
        //                    if (muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.B != -5000)
        //                        wsCadenaTemp.Range("AW" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.B;
        //                    if (muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.O != -5000)
        //                        wsCadenaTemp.Range("AX" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.numeroContenedores.O;

        //                    wsCadenaTemp.Range("Y" + Convert.ToString(17 + sampleIndex), "AA" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.ReceivedAmount;
        //                    wsCadenaTemp.Range("AB" + Convert.ToString(17 + sampleIndex), "AD" + Convert.ToString(17 + sampleIndex)).Value = muestrasPorHojas[hojaSampleIndex][sampleIndex].simpleSamplingIdentifier.LabNo;
        //                }

        //                if (horaLong > 0)
        //                {
        //                    long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //                    DateTime dateValue = new DateTime(beginTicks + horaLong * 10000);
        //                    wsCadenaTemp.Range("S" + Convert.ToString(17 + sampleIndex), "U" + Convert.ToString(17 + sampleIndex)).Value = dateValue.Hour + ":" + dateValue.Minute;
        //                }
        //            }
        //        }

        //    //-------------------------------------------------


        //    return Tuple.Create(paquetesPorHojas, muestrasPorHojas);
        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupBitacora1(Excel.Workbook workbook, SamplingOrder orden, Binnacle binnacle)
        //{
        //    var wsBitac1 = workbook.Sheets["BITACORA 1"];

        //    wsBitac1.Range("L1").Value = binnacle.bitacora2.folio;//XXXXXXXXXXXXX<>
        //    //wsBitac1.Range("K2", "L2").Value = binnacle.bitacora1.;//XXXXXXXXXXXXX<>
        //    wsBitac1.Range("B3", "E3").Value = orden.Sampler.User.BinnacleIdentifier;

        //    wsBitac1.Range("B6", "L6").Value = orden.Sampler.FullName;
        //    wsBitac1.Range("B7", "L7").Value = orden.Sampler.User.Job;
        //    wsBitac1.Range("B8", "L8").Value = orden.Sampler.User.Category;
        //    wsBitac1.Range("B9", "L9").Value = orden.Sampler.User.Subsidiary;

        //    long fecahInicioBitac = binnacle.FirstDate;
        //    if (fecahInicioBitac > 0)
        //    {
        //        long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue2 = new DateTime(beginTicks2 + fecahInicioBitac * 10000);
        //        wsBitac1.Range("B12", "C12").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //    }

        //    long fecahFinalBitac = binnacle.LastDate;
        //    if (fecahFinalBitac > 0)
        //    {
        //        long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue2 = new DateTime(beginTicks2 + fecahFinalBitac * 10000);
        //        wsBitac1.Range("B13", "C13").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //    }

        //    wsBitac1.Range("B14", "C14").Value = "1";//XXXXXXXXXXXXX<>   Del folio
        //    wsBitac1.Range("E14", "F14").Value = binnacle.bitacora2.folio;//XXXXXXXXXXXXX<>   Al folio
        //    wsBitac1.Range("B26", "E26").Value = orden.Creator.Employee.FullName;
        //    wsBitac1.Range("A26", "L32").Value = binnacle.bitacora1.observaciones;

        //    Employee samplerMS;
        //    Employee creatorMS;
        //    using (var context = new MyContext())
        //    {
        //        samplerMS = context.GetEmployee(orden.Sampler.EmployeeId);
        //        creatorMS = context.GetEmployee(orden.Creator.Employee.EmployeeId);
        //    }

        //    string firmaCreadorMS = "";
        //    string firmaMuestreadorMS = "";
        //    if (creatorMS != null)
        //        firmaCreadorMS = creatorMS.Signature;
        //    if (samplerMS != null)
        //        firmaMuestreadorMS = samplerMS.Signature;

        //    if (!String.IsNullOrEmpty(firmaCreadorMS))
        //    {
        //        string path = convertAndSave(firmaCreadorMS);
        //        wsBitac1.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 185, 257, 220, 70);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }
        //    if (!String.IsNullOrEmpty(firmaMuestreadorMS))
        //    {
        //        string path = convertAndSave(firmaMuestreadorMS);
        //        wsBitac1.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 592, 257, 220, 70);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }

        //}
        ////--------------------------------------------------------------------------------------
        //private static void setupBitacora2(Excel.Workbook workbook, SamplingOrder orden, SamplingPlan planMuestreo, QualityControl controlCalidad, Binnacle binnacle)
        //{
        //    var wsBitac2 = workbook.Sheets["BITACORA 2"];

        //    long fecahFBitac2 = binnacle.LastDate;
        //    if (fecahFBitac2 > 0)
        //    {
        //        long beginTicks2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        //        DateTime dateValue2 = new DateTime(beginTicks2 + fecahFBitac2 * 10000);
        //        wsBitac2.Range("B1", "C1").Value = dateValue2.Day + "/" + dateValue2.Month + "/" + dateValue2.Year;
        //    }

        //    wsBitac2.Range("G1", "I1").Value = orden.SamplingData.Identifier;
        //    wsBitac2.Range("L1").Value = binnacle.bitacora2.folio;
        //    wsBitac2.Range("B2", "L2").Value = orden.ClientData.SocialReason;
        //    wsBitac2.Range("B3", "L3").Value = orden.LocationData.Place;
        //    wsBitac2.Range("B4", "L4").Value = binnacle.bitacora2.personalAuxiliar;

        //    String parametrosComa = "";
        //    foreach (var workPaq in orden.WorkPackages)
        //        foreach (var item in workPaq.Packages)
        //            foreach (var parm in item.Parameters)
        //            {
        //                parametrosComa += parm.Identifier + ", ";
        //            }
        //    parametrosComa = parametrosComa.Remove(parametrosComa.LastIndexOf(","));
        //    wsBitac2.Range("B15", "L21").Value = parametrosComa;

        //    wsBitac2.Range("B22", "L26").Value = orden.Sampler.User.CalibrationKit.Name;
        //    wsBitac2.Range("B28", "G28").Value = orden.Sampler.User.CalibrationKit.Series;
        //    wsBitac2.Range("H28", "L28").Value = orden.Sampler.User.CalibrationKit.Model;

        //    wsBitac2.Range("B32", "C32").Value = controlCalidad.condElectricaDulce.marca;
        //    wsBitac2.Range("B33", "C33").Value = controlCalidad.condElectricaSalina.marca;
        //    wsBitac2.Range("B34", "C34").Value = controlCalidad.bufferPH4.marca;
        //    wsBitac2.Range("B35", "C35").Value = controlCalidad.bufferPH7.marca;
        //    wsBitac2.Range("B36", "C36").Value = controlCalidad.bufferPH10.marca;
        //    wsBitac2.Range("B37", "C37").Value = controlCalidad.potencialREDOX.marca;

        //    wsBitac2.Range("D32", "E32").Value = controlCalidad.condElectricaDulce.lote;
        //    wsBitac2.Range("D33", "E33").Value = controlCalidad.condElectricaSalina.lote;
        //    wsBitac2.Range("D34", "E34").Value = controlCalidad.bufferPH4.lote;
        //    wsBitac2.Range("D35", "E35").Value = controlCalidad.bufferPH7.lote;
        //    wsBitac2.Range("D36", "E36").Value = controlCalidad.bufferPH10.lote;
        //    wsBitac2.Range("D37", "E37").Value = controlCalidad.potencialREDOX.lote;

        //    wsBitac2.Range("F32", "G32").Value = controlCalidad.condElectricaDulce.caducidad;
        //    wsBitac2.Range("F33", "G33").Value = controlCalidad.condElectricaSalina.caducidad;
        //    wsBitac2.Range("F34", "G34").Value = controlCalidad.bufferPH4.caducidad;
        //    wsBitac2.Range("F35", "G35").Value = controlCalidad.bufferPH7.caducidad;
        //    wsBitac2.Range("F36", "G36").Value = controlCalidad.bufferPH10.caducidad;
        //    wsBitac2.Range("F37", "G37").Value = controlCalidad.potencialREDOX.caducidad;

        //    if (controlCalidad.condElectricaDulce.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H32").Value = controlCalidad.condElectricaDulce.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I32").Value = controlCalidad.condElectricaDulce.calibracionInicial.temperatura;

        //    if (controlCalidad.condElectricaSalina.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H33").Value = controlCalidad.condElectricaSalina.calibracionInicial.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I33").Value = controlCalidad.condElectricaSalina.calibracionInicial.temperatura;

        //    if (controlCalidad.bufferPH4.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H34").Value = controlCalidad.bufferPH4.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH4.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I34").Value = controlCalidad.bufferPH4.calibracionInicial.temperatura;

        //    if (controlCalidad.bufferPH7.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H35").Value = controlCalidad.bufferPH7.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH7.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I35").Value = controlCalidad.bufferPH7.calibracionInicial.temperatura;

        //    if (controlCalidad.bufferPH10.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H36").Value = controlCalidad.bufferPH10.calibracionInicial.VM;
        //    if (controlCalidad.bufferPH10.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I36").Value = controlCalidad.bufferPH10.calibracionInicial.temperatura;

        //    if (controlCalidad.potencialREDOX.calibracionInicial.VM != -5000)
        //        wsBitac2.Range("H37").Value = controlCalidad.potencialREDOX.calibracionInicial.VM;
        //    if (controlCalidad.potencialREDOX.calibracionInicial.temperatura != -5000)
        //        wsBitac2.Range("I37").Value = controlCalidad.potencialREDOX.calibracionInicial.temperatura;

        //    if (controlCalidad.condElectricaDulce.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K32").Value = controlCalidad.condElectricaDulce.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaDulce.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L32").Value = controlCalidad.condElectricaDulce.calibracionFinal.temperatura;

        //    if (controlCalidad.condElectricaSalina.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K33").Value = controlCalidad.condElectricaSalina.calibracionFinal.VM;
        //    if (controlCalidad.condElectricaSalina.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L33").Value = controlCalidad.condElectricaSalina.calibracionFinal.temperatura;

        //    if (controlCalidad.bufferPH4.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K34").Value = controlCalidad.bufferPH4.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH4.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L34").Value = controlCalidad.bufferPH4.calibracionFinal.temperatura;

        //    if (controlCalidad.bufferPH7.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K35").Value = controlCalidad.bufferPH7.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH7.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L35").Value = controlCalidad.bufferPH7.calibracionFinal.temperatura;

        //    if (controlCalidad.bufferPH10.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K36").Value = controlCalidad.bufferPH10.calibracionFinal.VM;
        //    if (controlCalidad.bufferPH10.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L36").Value = controlCalidad.bufferPH10.calibracionFinal.temperatura;

        //    if (controlCalidad.potencialREDOX.calibracionFinal.VM != -5000)
        //        wsBitac2.Range("K37").Value = controlCalidad.potencialREDOX.calibracionFinal.VM;
        //    if (controlCalidad.potencialREDOX.calibracionFinal.temperatura != -5000)
        //        wsBitac2.Range("L37").Value = controlCalidad.potencialREDOX.calibracionFinal.temperatura;

        //    foreach (ProtectionTool ptool in planMuestreo.equipoProteccionList)
        //    {
        //        switch (ptool.tipo)
        //        {
        //            case ProtectionToolsType.Casco:
        //                wsBitac2.Range("C39").Value = "X";
        //                break;
        //            case ProtectionToolsType.Mascarilla:
        //                wsBitac2.Range("J39").Value = "X";
        //                break;
        //            case ProtectionToolsType.Lentes:
        //                wsBitac2.Range("E39").Value = "X";
        //                break;
        //            case ProtectionToolsType.ChalecoSalvavidas:
        //                wsBitac2.Range("J40").Value = "X";
        //                break;
        //            case ProtectionToolsType.Overall:
        //                wsBitac2.Range("C40").Value = "X";
        //                break;
        //            case ProtectionToolsType.Botas:
        //                wsBitac2.Range("L39").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesCuero:
        //                wsBitac2.Range("H39").Value = "X";
        //                break;
        //            case ProtectionToolsType.Tyvex:
        //                wsBitac2.Range("L40").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesLatex:
        //                wsBitac2.Range("H40").Value = "X";
        //                break;
        //            case ProtectionToolsType.GuantesNitrilo:
        //                wsBitac2.Range("H41").Value = "X";
        //                break;
        //            case ProtectionToolsType.Arnes:
        //                wsBitac2.Range("E40").Value = "X";
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    wsBitac2.Range("A44", "L64").Value = binnacle.bitacora2.desarrolloMuestreo;
        //    wsBitac2.Range("C66", "D66").Value = binnacle.bitacora2.trasladoMuestraLab.tecnico;
        //    wsBitac2.Range("G66", "H66").Value = binnacle.bitacora2.trasladoMuestraLab.mensajeria;
        //    wsBitac2.Range("K66", "L66").Value = binnacle.bitacora2.trasladoMuestraLab.guiaNumero;

        //    Employee samplerMS;
        //    using (var context = new MyContext())
        //    {
        //        samplerMS = context.GetEmployee(orden.Sampler.EmployeeId);
        //    }

        //    string firmaMuestreadorMS = "";

        //    if (samplerMS != null)
        //        firmaMuestreadorMS = samplerMS.Signature;

        //    if (!String.IsNullOrEmpty(firmaMuestreadorMS))
        //    {
        //        string path = convertAndSave(firmaMuestreadorMS);
        //        wsBitac2.Shapes.AddPicture(path, MsoTriState.msoFalse, MsoTriState.msoCTrue, 618, 1067, 230, 75);//x,y,w,h
        //        if (File.Exists(path))
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //    }
        //}
        ////--------------------------------------------------------------------------------------
        //internal static void ConvertExcelToPdf(string excelFileIn, string pdfFileOut)
        //{
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    Excel.Application excel;
        //    Excel.Workbook workbook;
        //    excel = new Excel.Application();

        //    if (File.Exists(pdfFileOut))
        //        try
        //        {
        //            File.Delete(pdfFileOut);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("No se puede crear el archivo PDF.\nVerifique que no se encuentre abierto por otra aplicación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //    try
        //    {
        //        /*excel.Visible = false;
        //        excel.ScreenUpdating = false;*/
        //        excel.DisplayAlerts = false;

        //        /*FileInfo excelFile = new FileInfo(excelFileIn);

        //        string filename = excelFile.FullName;*/

        //        workbook = excel.Workbooks.Open(excelFileIn, Type.Missing,
        //        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing, Type.Missing, Type.Missing);
        //        //wbk.Activate();

        //        object outputFileName = pdfFileOut;
        //        Microsoft.Office.Interop.Excel.XlFixedFormatType fileFormat = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;

        //        // Save document into PDF Format
        //        workbook.ExportAsFixedFormat(fileFormat, outputFileName,
        //        Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing);

        //        object saveChanges = Microsoft.Office.Interop.Excel.XlSaveAction.xlDoNotSaveChanges;
        //        workbook.Close(saveChanges, Type.Missing, Type.Missing);
        //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(workbook);
        //        workbook = null;
        //    }
        //    catch (Exception)
        //    {

        //        //  MessageBox.Show("Unable to release the Object " + ex.ToString());
        //    }
        //    finally
        //    {
        //        excel.Quit();
        //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        //        workbook = null;
        //        excel = null;
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}
        ////--------------------------------------------------------------------------------------
        //private static string convertAndSave(string base64String)
        //{
        //    var r = new Random();
        //    var path = @ConfigurationManager.AppSettings["FileUploadDirectory"] + @"imagenes_binn\" + r.Next(10000) + "img.png";
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    Image image;
        //    using (Stream ms = new MemoryStream(imageBytes))
        //    {
        //        image = Image.FromStream(ms);
        //        image.Save(path);
        //    }
        //    return path;
        //}
        ////-----------------------------------
        //internal static int GetIDProcces(string nameProcces)
        //{

        //    try
        //    {
        //        Process[] asProccess = Process.GetProcessesByName(nameProcces);

        //        foreach (Process pProccess in asProccess)
        //        {
        //            if (pProccess.MainWindowTitle == "")
        //            {
        //                return pProccess.Id;
        //            }
        //        }

        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }
        //}
    }

    //public class ParamCadena
    //{
    //    public string paramPaqName = "";
    //    public int paramPaqID = 0;
    //    public bool isStandard;
    //}
    //public class MuestraCadena
    //{
    //    public SimpleSamplingIdentifier simpleSamplingIdentifier;
    //    public List<ParamVerification> parametrosMuestraList;
    //    public long fechaMostrar;
    //    public bool isMCInventada = false;
    //}
}