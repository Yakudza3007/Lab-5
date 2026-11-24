using System;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace ConsoleApp1
{
    internal class ExcelManager
    {
        private string _fileName;

        public ExcelManager(string fileName)
        {
            _fileName = fileName;
        }

        public List<Film> ReadFilms()
        {
            List<Film> list = new List<Film>();
            
            using (XLWorkbook workbook = new XLWorkbook(_fileName))
            {
                IXLWorksheet sheet = workbook.Worksheet(1);
                
                foreach (IXLRow row in sheet.RowsUsed().Skip(1))
                {
                    int id = row.Cell(1).GetValue<int>();
                    string title = row.Cell(2).GetValue<string>();
                    int dirId = row.Cell(3).GetValue<int>();
                    int year = row.Cell(4).GetValue<int>();
                    int genId = row.Cell(5).GetValue<int>();
                    int dur = row.Cell(6).GetValue<int>();
                    decimal bud = row.Cell(7).GetValue<decimal>();
                    decimal box = row.Cell(8).GetValue<decimal>();
                    
                    list.Add(new Film(id, title, dirId, year, 
                        genId, dur, bud, box));
                }
            }
            return list;
        }

        public void SaveFilms(List<Film> films)
        {
            using (XLWorkbook workbook = new XLWorkbook(_fileName))
            {
                IXLWorksheet sheet = workbook.Worksheet(1);
                sheet.Rows(2, sheet.LastRowUsed().RowNumber())
                    .Delete();
                     
                int row = 2;
                foreach (Film f in films)
                {
                    sheet.Cell(row, 1).Value = f.Id;
                    sheet.Cell(row, 2).Value = f.Title;
                    sheet.Cell(row, 3).Value = f.DirectorId;
                    sheet.Cell(row, 4).Value = f.Year;
                    sheet.Cell(row, 5).Value = f.GenreId;
                    sheet.Cell(row, 6).Value = f.Duration;
                    sheet.Cell(row, 7).Value = f.Budget;
                    sheet.Cell(row, 8).Value = f.BoxOffice;
                    row++;
                }
                workbook.Save();
            }
        }
    }
}
