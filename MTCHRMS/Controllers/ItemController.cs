using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework.Inventory;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using MTC.Models.Inventory;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;

namespace MTCHRMS.Controllers
{

    //[RoutePrefix("/api/item")]
    public class ItemController : ApiController
    {
        public IItemsRepository _repo;

        public ItemController(IItemsRepository repo)
        {
            _repo = repo;
        }

        //[Route("api/item/")]
        //[HttpGet]
        //[Authorize]
        //public Task<IQueryable<Item>> Get()
        //{
        //    // IQueryable filter data inside sql query and on database side get specified filter results only, 
        //    //where as IEnumerable get all data from databse and filter it on client side

        //    //System.Threading.Thread.Sleep(1000);
        //    var items = _repo.GetItems();

        //    return items;
        //}

        [Route("api/item/")]
        [HttpGet]
        [Authorize]
        public Task<IQueryable<ItemModel>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var items = _repo.GetItemsModel();

            return items;
        }

        [Route("api/item/getExcelFile")]
        [HttpGet]
        //[Authorize]
        public async Task<HttpResponseMessage> GetExcelFile([FromUri]Item item)
        {
            var items = await _repo.GetItemSearch(item);

            var xlPackage = new ExcelPackage();
            var oSheet = xlPackage.Workbook.Worksheets.Add("Item Report");
            oSheet.Cells["A2"].Value = "Hello world";
            oSheet.Cells["D5"].LoadFromCollection(items);
            var stream = xlPackage.GetAsByteArray();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream)
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "abc1.xlsx"
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;

        }

        [Route("api/item/getPdfFile")]
        [HttpGet]
        //[Authorize]
        public async Task<HttpResponseMessage> GetPdfFile([FromUri]Item item)
        {
            var fullName = string.Empty;
            if (Request.Headers.Contains("name"))
            {
                fullName = Request.Headers.GetValues("name").First();
            }
            var items = await _repo.GetItemSearch(item);

            var report = new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata
                {
                    Author = "Sits",
                    Application = "MTC-Inventory",
                    Keywords = ", .",
                    Subject = "Item List",
                    Title = "Item List"
                });
            })
            .DefaultFonts(fonts =>
            {
                //fonts.Path(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf",
                //                  Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\verdana.ttf");
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter("Print By :: " + fullName + " , @ " + DateTime.Now.ToString("dd MMM yyyy HH:mm"));
            })
            .PagesHeader(header =>
            {
                header.PdfFont.Size = 30;
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    
                    defaultHeader.ImagePath(HttpRuntime.AppDomainAppPath + "\\Content\\img\\photo.jpg");
                    defaultHeader.Message("Report : Item Listing ");
                    defaultHeader.MessageFontStyle(DocumentFontStyle.Bold);
                    
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.ProfessionalTemplate);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                dataSource.StronglyTypedList(items);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                //summarySettings.OverallSummarySettings("Summary (Total Stock)");
                //summarySettings.OverallSummarySettings("Summary (Total Items)");
                //summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                //summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("SN.");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Code);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Justified);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Code");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Justified);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(6);
                    column.HeaderCell("Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.PartNo);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Part No");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Type);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(3);
                    column.HeaderCell("Class");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Category);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(3);
                    column.HeaderCell("Category");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Store);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(3);
                    column.HeaderCell("Store");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ItemModel>(x => x.Stock);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(1);
                    column.HeaderCell("Stock");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    //column.AggregateFunction(aggregateFunction =>
                    //{
                    //    aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                    //    aggregateFunction.DisplayFormatFormula(obj => obj == null ? string.Empty : string.Format("{0:n0}", obj));
                    //});
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            });
            //.Export(export =>
            //{
            //    export.ToExcel();
            //    export.ToCsv();
            //    export.ToXml();
            //});
            string fileName = Guid.NewGuid() + ".pdf";
            string filePath = String.Format("{0}\\temp\\{1}", HttpRuntime.AppDomainAppPath, fileName);  // HttpRuntime.AppDomainAppPath + "\\temp\\" + fileName;
            report.Generate(data => data.AsPdfFile(filePath));
            //var stream = report.gen

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(File.ReadAllBytes(filePath))
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            File.Delete(filePath);
            return result;

        }

        [Route("api/item/GetSingleItem")]
        [HttpGet]
        [Authorize]
        public IQueryable<Item> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var item = _repo.GetItemDetail(id);

            if (item == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return item;
        }

        [Route("api/item/GetItemPicture/{id}")]
        [HttpGet]
        //[Authorize]
        public async Task<ItemModel> GetItemPicture(int id)
        {
            var item = await _repo.GetItemPicture(id);

            if (item == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.inva);
            }
            return item;
        }


        [Route("api/item/ItemDepartments")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemDepartment>> GetItemDepartments()
        {
            var departments = _repo.GetItemDepartments();
            return await departments.ToListAsync();
        }

        [Route("api/item/ItemSuppliers")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemSupplier>> GetItemSuppliers()
        {
            var suppliers = _repo.GetItemSuppliers();
            return await suppliers.ToListAsync();
        }

        [Route("api/item/{id}/Suppliers")]
        [HttpGet]
        [Authorize]
        public async Task<List<Supplier>> GetSuppliersByItemId(int id)
        {
            var suppliers = await _repo.GetSuppliersByItemId(id);
            return await suppliers.ToListAsync();
        }


        [Route("api/item/ItemYears")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemYear>> GetItemYears()
        {
            var years = _repo.GetItemYears();
            return await years.ToListAsync();
        }

        [Route("api/item/ItemManufacturers")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemManufacturer>> GetItemManufacturers()
        {
            var manufacturers = _repo.GetItemManufactuers();
            return await manufacturers.ToListAsync();
        }


        [Route("api/item/GetItemSearch")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<ItemModel>> GetItemSearch([FromUri]Item item)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            var items = await _repo.GetItemSearch(item);
            return items;
        }


        [Route("api/item/")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Post([FromBody] Item newItem)
        {
            if (ModelState.IsValid)
            {
                if (newItem.ItemId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItem.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    newItem.CreatedOn = DateTime.Now;

                    if (_repo.CheckItemDuplicate(newItem) != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItem);
                    }

                    if (_repo.AddItem(newItem) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newItem);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItem.ItemId!=0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItem.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItem.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateItem(newItem) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newItem);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Route("api/item/addItemStock")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostItemStock([FromBody] ItemStockAdd newStockAdd)
        {
            if (ModelState.IsValid)
            {
                if (newStockAdd.ItemStockAddId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newStockAdd.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    newStockAdd.CreatedOn = DateTime.Now;

                    newStockAdd.ComputerCode = "Auto one";
                    //if (_repo.CheckItemDuplicate(newItem) != 0)
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.Found, newItem);
                    //}

                    if (_repo.AddItemStock(newStockAdd))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newStockAdd);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newStockAdd.ItemStockAddId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newStockAdd.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newStockAdd.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateItemStock(newStockAdd) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newStockAdd);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }


        [Route("api/item/addItemStockSerials")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostItemStockSerials([FromBody] List<ItemStockSerial> newItemSerials)
        {
            if (ModelState.IsValid)
            {
                if (newItemSerials.Capacity > 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        foreach (var itemSerial in newItemSerials)
                        {
                            itemSerial.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                            itemSerial.CreatedOn = DateTime.Now;
                        }
                    }

                    //if (_repo.CheckItemDuplicate(newItem) != 0)
                    //{
                      //  return Request.CreateResponse(HttpStatusCode.Found, newItem);
                    //}

                    if (_repo.AddItemStockSerials(newItemSerials) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Route("api/item/getItemStock/{id}")]
        [HttpGet]
        [Authorize]
        public Task<ItemStockAdd> GetItemStock(int id)
        {
            var stock = _repo.GetItemStock(id);

            return stock;
        }

        [Route("api/item/getItemSerial/{id}")]
        [HttpGet]
        [Authorize]
        public Task<IQueryable<ItemStockSerial>> GetItemSerials(int id)
        {
            var items = _repo.GetItemStockSerialsByItemId(id);

            return items;
        }

        [Route("api/item/getItemSerialStock/{id}")]
        [HttpGet]
        [Authorize]
        public Task<IQueryable<ItemStockSerial>> GetItemSerialsStock(int id)
        {
            var items = _repo.GetItemStockSerialsByStockAddId(id);

            return items;
        }

        [Route("api/item/updateItemStockSerial")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostItemStockSerial([FromBody] ItemStockSerial updateStockSerial)
        {
            if (ModelState.IsValid)
            {
                if (updateStockSerial.ItemStockSerialId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        updateStockSerial.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    updateStockSerial.ModifiedOn = DateTime.Now;


                    if (_repo.UpdateItemSerial(updateStockSerial) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, updateStockSerial);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Route("api/item/upload")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Upload()
        {
            try
            {

                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }
                HttpPostedFile _file = HttpContext.Current.Request.Files[0];
                
                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // Remove this line as well as GetFormData method if you're not 
                // sending any form data with your upload request
                int paramData = GetFormData<int>(result);

                var memstream = new MemoryStream();

                _file.InputStream.CopyTo(memstream);
                var item = _repo.GetItem(paramData);
                item.ItemPicture = memstream.ToArray();

                if (_repo.UpdateItemImage(ref item) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, item);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                //var originalFileName = GetDeserializedFileName(result.FileData.First());

                // uploadedFileInfo object will give you some additional stuff like file length,
                // creation time, directory name, a few filesystem methods etc..
                //var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);


                // Through the request response you can return an object to the Angular controller
                // You will be able to access this in the .success callback through its data attribute
                // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
                //var returnData = "ReturnTest";
                //return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
        }

        [Route("api/item/attachment")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> UploadAttachment()
        {
            try
            {

                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }
                HttpPostedFile _file = HttpContext.Current.Request.Files[0];
                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                int paramData = GetFormData<int>(result);

                ItemAttachment attachment = new ItemAttachment()
                {
                    ItemId = paramData,
                    FileName = Path.GetFileName(_file.FileName),
                    FileType =  Path.GetExtension(_file.FileName),
                    FileIcon = Path.GetExtension(_file.FileName).Substring(1),
                    AttachmentGuid = Guid.NewGuid().ToString(),
                    CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                    CreatedOn =  DateTime.Now
                    
                };

                if (_repo.AddItemAttachment(ref attachment) && _repo.Save())
                {
                    attachment = await _repo.GetAttachment(attachment.AttachmentId);

                    //_file.SaveAs(Path.Combine(attachment.StoragePath.FullPath, attachment.AttachmentId + Path.GetExtension(_file.FileName)));
                    _file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("/attachments/inventory/items/" +  attachment.AttachmentGuid + Path.GetExtension(_file.FileName)));
                    return Request.CreateResponse(HttpStatusCode.Created, attachment);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
        }
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private int GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    //return JsonConvert.DeserializeObject<T>(unescapedFormData);
                    return Convert.ToInt32(unescapedFormData); // JsonConvert.DeserializeObject(unescapedFormData);
                }
            }

            return 0;
        }


        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] Item updateItem)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateItem.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateItem.ModifiedOn = DateTime.Now;

                if (_repo.UpdateItem(updateItem) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateItem);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }


        [ActionName("PostItemDepartment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemDepartment([FromBody] ItemDepartment newItemDepartment)
        {
            if (ModelState.IsValid)
            {
                if (newItemDepartment.ItemDepartmentId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemDepartment.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemDepartment.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemDepartmentDuplicate(newItemDepartment.ItemId, 0, newItemDepartment.DepartmentId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemDepartment);
                    }
                    if (_repo.AddItemDepartment(newItemDepartment) && _repo.Save())
                    {
                        newItemDepartment = _repo.GetItemDepartment(newItemDepartment.ItemDepartmentId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemDepartment);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemDepartment.ItemDepartmentId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemDepartment.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemDepartment.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemDepartmentDuplicate(newItemDepartment.ItemId, newItemDepartment.ItemDepartmentId, newItemDepartment.DepartmentId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemDepartment);
                    }

                    if (_repo.UpdateItemDepartment(newItemDepartment) && _repo.Save())
                    {
                        newItemDepartment = _repo.GetItemDepartment(newItemDepartment.ItemDepartmentId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemDepartment);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemDepartment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemDepartment([FromBody] ItemDepartment deleteDepartment)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteItemDepartment(deleteDepartment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteDepartment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }


        [ActionName("PostItemYear")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemYear([FromBody] ItemYear newItemYear)
        {
            if (ModelState.IsValid)
            {
                if (newItemYear.ItemYearId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemYear.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemYear.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemYearDuplicate(newItemYear.ItemId, 0, newItemYear.YearId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemYear);
                    }


                    if (_repo.AddItemYear(newItemYear) && _repo.Save())
                    {
                        newItemYear = _repo.GetItemYear(newItemYear.ItemYearId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemYear);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemYear.ItemYearId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemYear.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemYear.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemYearDuplicate(newItemYear.ItemId, newItemYear.ItemYearId, newItemYear.YearId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemYear);
                    }

                    if (_repo.UpdateItemYear(newItemYear) && _repo.Save())
                    {
                        newItemYear = _repo.GetItemYear(newItemYear.ItemYearId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemYear);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemYear")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemYear([FromBody] ItemYear deleteYear)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteItemYear(deleteYear) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteYear);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostItemSupplier")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemSupplier([FromBody] ItemSupplier newItemSupplier)
        {
            if (ModelState.IsValid)
            {
                if (newItemSupplier.ItemSupplierId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemSupplier.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemSupplier.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemSupplierDuplicate(newItemSupplier.ItemId, 0, newItemSupplier.SupplierId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemSupplier);
                    }


                    if (_repo.AddItemSupplier(newItemSupplier) && _repo.Save())
                    {
                        newItemSupplier = _repo.GetItemSupplier(newItemSupplier.ItemSupplierId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemSupplier);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemSupplier.ItemSupplierId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemSupplier.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemSupplier.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemSupplierDuplicate(newItemSupplier.ItemId, newItemSupplier.ItemSupplierId, newItemSupplier.SupplierId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemSupplier);
                    }

                    if (_repo.UpdateItemSupplier(newItemSupplier) && _repo.Save())
                    {
                        newItemSupplier = _repo.GetItemSupplier(newItemSupplier.ItemSupplierId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemSupplier);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemSupplier")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemSupplier([FromBody] ItemSupplier deleteSupplier)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteItemSupplier(deleteSupplier) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteSupplier);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostItemManufacturer")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemManufacturer([FromBody] ItemManufacturer newItemManufacturer)
        {
            if (ModelState.IsValid)
            {
                if (newItemManufacturer.ItemManufacturerId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemManufacturer.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemManufacturer.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemManufacturerDuplicate(newItemManufacturer.ItemId, 0, newItemManufacturer.ManufacturerId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemManufacturer);
                    }

                    if (_repo.AddItemManufacturer(newItemManufacturer) && _repo.Save())
                    {
                        newItemManufacturer = _repo.GetItemManufactuer(newItemManufacturer.ItemManufacturerId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemManufacturer);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemManufacturer.ItemManufacturerId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemManufacturer.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemManufacturer.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemManufacturerDuplicate(newItemManufacturer.ItemId, newItemManufacturer.ItemManufacturerId, newItemManufacturer.ManufacturerId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemManufacturer);
                    }

                    if (_repo.UpdateItemManufacturer(newItemManufacturer) && _repo.Save())
                    {
                        newItemManufacturer = _repo.GetItemManufactuer(newItemManufacturer.ItemManufacturerId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemManufacturer);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemManufacturer")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemManufacturer([FromBody] ItemManufacturer deleteManufacturer)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteItemManufacturer(deleteManufacturer) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, deleteManufacturer);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemAttachment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemAttachment([FromBody] ItemAttachment deleteAttachment)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteItemAttachment(deleteAttachment) && _repo.Save())
                {
                    File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("/attachments/inventory/items/" + deleteAttachment.AttachmentGuid + deleteAttachment.FileType));
                    return Request.CreateResponse(HttpStatusCode.OK, deleteAttachment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [Route("api/item/BarcodeData/{serialno}")]
        [HttpGet]
        public HttpResponseMessage GetBarcodeData(string serialno)
        {
            try
            {
                var _str = new StringBuilder("START" + Environment.NewLine);

                _str.Append(@"^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ" + Environment.NewLine);
                _str.Append("^XA" + Environment.NewLine);
                _str.Append(@"^MMT" + Environment.NewLine);
                _str.Append(@"^LL0300" + Environment.NewLine);
                _str.Append(@"^PW1050" + Environment.NewLine);
                _str.Append(@"^LS0" + Environment.NewLine);
                _str.Append(@"^BY3,3,84^FT60,148^BCN,,N,N" + Environment.NewLine);
                _str.Append(string.Format(@"^FD>;{0}^FS", serialno) + Environment.NewLine);
                _str.Append(@"^FT129,52^A0N,22,43^FH\^FDMTC-INV^FS" + Environment.NewLine);
                _str.Append(string.Format(@"^FT108,183^A0N,33,33^FH\^FD{0}^FS", serialno) + Environment.NewLine);
                _str.Append(@"^PQ1,0,1,Y^XZ" + Environment.NewLine);

                _str.Append(@"END" + Environment.NewLine);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(_str.ToString());

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                result.Content.Headers.ContentDisposition.FileName = Guid.NewGuid().ToString() + ".ps";
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

        }


        [Route("api/item/BarcodeDataItemId/{itemId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetBarcodeDataByItemId(int itemId)
        {
            try
            {
                var _str = new StringBuilder("START" + Environment.NewLine);

                var serials = await _repo.GetItemStockSerialsByItemId(itemId);

                foreach (var ser in serials)
                {
                    if (ser.SerialNo.Length > 0)
                    {
                        _str.Append(@"^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ" + Environment.NewLine);
                        _str.Append("^XA" + Environment.NewLine);
                        _str.Append(@"^MMT" + Environment.NewLine);
                        _str.Append(@"^LL0300" + Environment.NewLine);
                        _str.Append(@"^PW1050" + Environment.NewLine);
                        _str.Append(@"^LS0" + Environment.NewLine);
                        _str.Append(@"^BY3,3,84^FT60,148^BCN,,N,N" + Environment.NewLine);
                        _str.Append(string.Format(@"^FD>;{0}^FS", ser.SerialNo) + Environment.NewLine);
                        _str.Append(@"^FT129,52^A0N,22,43^FH\^FDMTC-INV^FS" + Environment.NewLine);
                        _str.Append(string.Format(@"^FT108,183^A0N,33,33^FH\^FD{0}^FS", ser.SerialNo) + Environment.NewLine);
                        _str.Append(@"^PQ1,0,1,Y^XZ" + Environment.NewLine);
                    }
                }

                _str.Append(@"END" + Environment.NewLine);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(_str.ToString());

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                result.Content.Headers.ContentDisposition.FileName = Guid.NewGuid().ToString() + ".ps";
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/item/BarcodeDataStockId/{stockId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetBarcodeDataByStockId(int stockId)
        {
            try
            {
                var _str = new StringBuilder("START" + Environment.NewLine);

                var serials = await _repo.GetItemStockSerialsByStockAddId(stockId);

                foreach (var ser in serials)
                {
                    if (ser.SerialNo.Length > 0)
                    {
                        _str.Append(@"^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ" + Environment.NewLine);
                        _str.Append("^XA" + Environment.NewLine);
                        _str.Append(@"^MMT" + Environment.NewLine);
                        _str.Append(@"^LL0300" + Environment.NewLine);
                        _str.Append(@"^PW1050" + Environment.NewLine);
                        _str.Append(@"^LS0" + Environment.NewLine);
                        _str.Append(@"^BY3,3,84^FT60,148^BCN,,N,N" + Environment.NewLine);
                        _str.Append(string.Format(@"^FD>;{0}^FS", ser.SerialNo) + Environment.NewLine);
                        _str.Append(@"^FT129,52^A0N,22,43^FH\^FDMTC-INV^FS" + Environment.NewLine);
                        _str.Append(string.Format(@"^FT108,183^A0N,33,33^FH\^FD{0}^FS", ser.SerialNo) + Environment.NewLine);
                        _str.Append(@"^PQ1,0,1,Y^XZ" + Environment.NewLine);
                    }
                }

                _str.Append(@"END" + Environment.NewLine);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(_str.ToString());

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                result.Content.Headers.ContentDisposition.FileName = Guid.NewGuid().ToString() + ".ps";
                return result;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/item/ItemUsers")]
        [HttpGet]
        [Authorize]
        public async Task<IQueryable<ItemUsersModel>> GetItemUsers()
        {
            var users = await _repo.GetInventoryUser();
            return users;
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }

        
    }
}
