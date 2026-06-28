using Accounting.Application.DTOs.Accessibility;
using Accounting.Application.DTOs.Barcode;
using Accounting.Application.DTOs.Company;
using Accounting.Application.DTOs.Currency;
using Accounting.Application.DTOs.Kala;
using Accounting.Application.DTOs.KalaSerial;
using Accounting.Application.DTOs.Karbar;
using Accounting.Application.DTOs.Khadamat;
using Accounting.Application.DTOs.Moshtari;
using Accounting.Application.DTOs.MoshtariGroup;
using Accounting.Application.DTOs.Persenel;
using Accounting.Application.DTOs.ProductGroup;
using Accounting.Application.DTOs.Shobe;
using Accounting.Application.DTOs.Tax;
using Accounting.Application.DTOs.User;
using Accounting.Application.DTOs.UserPermission;
using Accounting.Application.DTOs.VahedKala;
using Accounting.Application.DTOs.Warehouse;
using Accounting.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Company
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Karbar, KarbarDto>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.DefaultShobe, opt => opt.MapFrom(src => src.DefaultShobe))
            .ForMember(dest => dest.BranchAccesses, opt => opt.MapFrom(src => src.Accessibilities));
        CreateMap<CreateKarbarDto, Karbar>();
        CreateMap<UpdateKarbarDto, Karbar>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Karbar, AccessibilityKarbarDto>();

        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Moshtari, MoshtariDto>();
        CreateMap<CreateMoshtariDto, Moshtari>();
        CreateMap<UpdateMoshtariDto, Moshtari>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<MoshtariGroup, MoshtariGroupDto>();
        CreateMap<CreateMoshtariGroupDto, MoshtariGroup>();
        CreateMap<UpdateMoshtariGroupDto, MoshtariGroup>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Persenel, PersenelDto>();
        CreateMap<CreatePersenelDto, Persenel>();
        CreateMap<UpdatePersenelDto, Persenel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Currency, CurrencyDto>();
        CreateMap<CreateCurrencyDto, Currency>();
        CreateMap<UpdateCurrencyDto, Currency>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CurrencyRate, CurrencyRateDto>();
        CreateMap<CreateCurrencyRateDto, CurrencyRate>();
        CreateMap<UpdateCurrencyRateDto, CurrencyRate>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<CreateWarehouseDto, Warehouse>();
        CreateMap<UpdateWarehouseDto, Warehouse>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<WarehouseReceipt, WarehouseReceiptDto>();
        CreateMap<WarehouseReceiptItem, WarehouseReceiptItemDto>();
        CreateMap<CreateWarehouseReceiptDto, WarehouseReceipt>();
        CreateMap<CreateWarehouseReceiptItemDto, WarehouseReceiptItem>();
        CreateMap<UpdateWarehouseReceiptDto, WarehouseReceipt>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<WarehouseDispatch, WarehouseDispatchDto>();
        CreateMap<WarehouseDispatchItem, WarehouseDispatchItemDto>();
        CreateMap<CreateWarehouseDispatchDto, WarehouseDispatch>();
        CreateMap<CreateWarehouseDispatchItemDto, WarehouseDispatchItem>();
        CreateMap<UpdateWarehouseDispatchDto, WarehouseDispatch>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<WarehouseTransfer, WarehouseTransferDto>();
        CreateMap<WarehouseTransferItem, WarehouseTransferItemDto>();
        CreateMap<CreateWarehouseTransferDto, WarehouseTransfer>();
        CreateMap<CreateWarehouseTransferItemDto, WarehouseTransferItem>();

        CreateMap<WarehouseReturn, WarehouseReturnDto>();
        CreateMap<WarehouseReturnItem, WarehouseReturnItemDto>();
        CreateMap<CreateWarehouseReturnDto, WarehouseReturn>();
        CreateMap<CreateWarehouseReturnItemDto, WarehouseReturnItem>();

        CreateMap<WarehouseDamage, WarehouseDamageDto>();
        CreateMap<CreateWarehouseDamageDto, WarehouseDamage>();

        CreateMap<WarehouseManager, WarehouseManagerDto>()
            .ForMember(dest => dest.KarbarId, opt => opt.MapFrom(src => src.MoshtariId));
        CreateMap<CreateWarehouseManagerDto, WarehouseManager>()
            .ForMember(dest => dest.MoshtariId, opt => opt.MapFrom(src => src.KarbarId));

        CreateMap<WarehouseStock, WarehouseStockDto>();
        CreateMap<UpdateWarehouseStockDto, WarehouseStock>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Shobe, ShobeDto>();
        CreateMap<CreateShobeDto, Shobe>();
        CreateMap<UpdateShobeDto, Shobe>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Accessibility, AccessibilityDto>()
            .ForMember(dest => dest.Karbar, opt => opt.MapFrom(src => src.Karbar))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.Shobe, opt => opt.MapFrom(src => src.Shobe));
        CreateMap<CreateAccessibilityDto, Accessibility>();

        // UserPermission (co-leader cross-company permissions)
        CreateMap<UserPermission, UserPermissionDto>(

            ).ForMember(des => des.Company, opt => opt.MapFrom(src => src.Company)).ForMember(des => des.User, opt => opt.MapFrom(src => src.User));
        CreateMap<CreateUserPermissionDto, UserPermission>();

        // VahedKala
        CreateMap<VahedKala, VahedKalaDto>();
        CreateMap<CreateVahedKalaDto, VahedKala>();
        CreateMap<UpdateVahedKalaDto, VahedKala>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ProductGroup
        CreateMap<ProductGroup, ProductGroupDto>();
        CreateMap<CreateProductGroupDto, ProductGroup>();
        CreateMap<UpdateProductGroupDto, ProductGroup>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Kala
        CreateMap<Kala, KalaDto>();
        CreateMap<CreateKalaDto, Kala>();
        CreateMap<UpdateKalaDto, Kala>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Barcode
        CreateMap<Barcode, BarcodeDto>();
        CreateMap<CreateBarcodeDto, Barcode>();
        CreateMap<UpdateBarcodeDto, Barcode>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // KalaSerial
        CreateMap<KalaSerial, KalaSerialDto>();
        CreateMap<CreateKalaSerialDto, KalaSerial>();
        CreateMap<UpdateKalaSerialDto, KalaSerial>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Khadamat
        CreateMap<Khadamat, KhadamatDto>();
        CreateMap<CreateKhadamatDto, Khadamat>();
        CreateMap<UpdateKhadamatDto, Khadamat>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Tax
        CreateMap<Tax, TaxDto>();
        CreateMap<CreateTaxDto, Tax>();
        CreateMap<UpdateTaxDto, Tax>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
