using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;
using MTC.Models.Inventory;

namespace MTCHRMS.DC
{
    public interface IItemsRepository
    {
        Task<IQueryable<ItemModel>> GetItemsModel();

        Task<ItemModel> GetItemModelDetail(int id);

        Task<ItemModel> GetItemPicture(int id);

        Task<IQueryable<Item>> GetItems();

        //IQueryable<Department> GetDepartments();
        IQueryable<Item> GetItemDetail(int id);

        Item GetItem(int id);


        /// <summary>
        /// Get All Item Departments
        /// </summary>
        /// <returns></returns>
        IQueryable<ItemDepartment> GetItemDepartments();

        IQueryable<ItemDepartment> GetItemDepartment(int itemDepartmentId);

        /// <summary>
        /// Get All Item Years
        /// </summary>
        /// <returns></returns>
        IQueryable<ItemYear> GetItemYears();

        IQueryable<ItemYear> GetItemYear(int itemYearId);

        /// <summary>
        /// Get All Item Suppliers
        /// </summary>
        /// <returns></returns>
        IQueryable<ItemSupplier> GetItemSuppliers();

        Task<IQueryable<Supplier>> GetSuppliersByItemId(int id);

        IQueryable<ItemSupplier> GetItemSupplier(int itemSupplierId);

        /// <summary>
        /// Get All Item Manufacturers
        /// </summary>
        /// <returns></returns>
        IQueryable<ItemManufacturer> GetItemManufactuers();

        /// <summary>
        /// Get Isingle Item Manufacture by Item Manufacturer Id
        /// </summary>
        /// <param name="itemManufacturerId"></param>
        /// <returns></returns>
        IQueryable<ItemManufacturer> GetItemManufactuer(int itemManufacturerId);

        Task<List<ItemModel>> GetItemSearch(Item item);

        bool Save();

        Int32 CheckItemDuplicate(Item newItem);

        bool AddItem(Item newItem);
        bool UpdateItem(Item updateItem);

        bool UpdateItemImage(ref Item item);


        bool AddItemStock(ItemStockAdd newItemStock);

        bool UpdateItemStock(ItemStockAdd updateItemStock);

        Task<ItemStockAdd> GetItemStock(int id);

        ItemStockSerial GetItemStockSerialsBySerialId(int serialId);

        Task<IQueryable<ItemStockSerial>> GetItemStockSerialsByItemId(int itemId);

        Task<IQueryable<ItemStockSerial>> GetItemStockSerialsByStockAddId(int stockAddId);

        bool AddItemStockSerials(List<ItemStockSerial> itemStockSerials);

        bool UpdateItemSerials(List<ItemStockSerial> newItemSerials);

        bool UpdateItemSerial(ItemStockSerial newItemSerials);


        bool CheckItemDepartmentDuplicate(int itemId, int itemDepartmentId, int departmentId);
        bool AddItemDepartment(ItemDepartment newItemDepartment);
        bool UpdateItemDepartment(ItemDepartment updateItemDepartment);
        bool DeleteItemDepartment(ItemDepartment deleteItemDepartment);


        bool CheckItemYearDuplicate(int itemId, int itemYearId, int yearId);
        bool AddItemYear(ItemYear newItemYear);
        bool UpdateItemYear(ItemYear updateItemYear);
        bool DeleteItemYear(ItemYear deleteItemYear);

        bool CheckItemSupplierDuplicate(int itemId, int itemSupplierId, int supplierId);
        bool AddItemSupplier(ItemSupplier newItemSupplier);
        bool UpdateItemSupplier(ItemSupplier updateItemSupplier);
        bool DeleteItemSupplier(ItemSupplier deleteItemSupplier);

        bool CheckItemManufacturerDuplicate(int itemId, int itemManufacturerId, int manufacturerId);
        bool AddItemManufacturer(ItemManufacturer newItemManufacturer);
        bool UpdateItemManufacturer(ItemManufacturer updateItemManufacturer);
        bool DeleteItemManufacturer(ItemManufacturer deleteItemManufacturer);

        bool AddItemAttachment(ref ItemAttachment newItemAttachment);
        bool DeleteItemAttachment(ItemAttachment deleteItemAttachment);


        bool DeleteItemAttachment(int id);

        Task<ItemAttachment> GetAttachment(int attachmentId);

        Task<IQueryable<ItemUsersModel>> GetInventoryUser();
    }
}
