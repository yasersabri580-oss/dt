using AutoMapper;
using Doctor.Application.DTOs.AboutHighlight;
using Doctor.Application.DTOs.Achievement;
using Doctor.Application.DTOs.AppointmentOption;
using Doctor.Application.DTOs.Article;
using Doctor.Application.DTOs.ContactInfo;
using Doctor.Application.DTOs.Doctor;
using Doctor.Application.DTOs.DoctorProfile;
using Doctor.Application.DTOs.DoctorService;
using Doctor.Application.DTOs.Faq;
using Doctor.Application.DTOs.HeroImage;
using Doctor.Application.DTOs.Qualification;
using Doctor.Application.DTOs.RefreshToken;
using Doctor.Application.DTOs.Review;
using Doctor.Application.DTOs.SocialLink;
using Doctor.Application.DTOs.Stat;
using Doctor.Application.DTOs.TechnologyHighlight;
using Doctor.Application.DTOs.User;

using Doctor.Domain.Entities;

namespace Doctor.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ── Users ────────────────────────────────────────────────────────
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Refresh Tokens ───────────────────────────────────────────────
        CreateMap<RefreshToken, RefreshTokenDto>();
        CreateMap<CreateRefreshTokenDto, RefreshToken>();

        
        // ── Doctors ──────────────────────────────────────────────────────
        CreateMap<Domain.Entities.Doctor, DoctorDto>();
        CreateMap<Domain.Entities.Doctor, DoctorWithProfileDto>()
            .ForMember(d => d.Profile, opt => opt.MapFrom(s => s.Profile));
        CreateMap<CreateDoctorDto, Domain.Entities.Doctor>();
        CreateMap<UpdateDoctorDto, Domain.Entities.Doctor>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Doctor Profiles ─────────────────────────────────────────────
        CreateMap<DoctorProfile, DoctorProfileDto>();
        CreateMap<CreateDoctorProfileDto, DoctorProfile>();
        CreateMap<UpdateDoctorProfileDto, DoctorProfile>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── About Highlights ─────────────────────────────────────────────
        CreateMap<AboutHighlight, AboutHighlightDto>();
        CreateMap<CreateAboutHighlightDto, AboutHighlight>();
        CreateMap<UpdateAboutHighlightDto, AboutHighlight>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Achievements ─────────────────────────────────────────────────
        CreateMap<Achievement, AchievementDto>();
        CreateMap<CreateAchievementDto, Achievement>();
        CreateMap<UpdateAchievementDto, Achievement>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Appointment Options ──────────────────────────────────────────
        CreateMap<AppointmentOption, AppointmentOptionDto>();
        CreateMap<CreateAppointmentOptionDto, AppointmentOption>();
        CreateMap<UpdateAppointmentOptionDto, AppointmentOption>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Articles ─────────────────────────────────────────────────────
        CreateMap<Article, ArticleDto>();
        CreateMap<CreateArticleDto, Article>();
        CreateMap<UpdateArticleDto, Article>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Contact Info ─────────────────────────────────────────────────
        CreateMap<ContactInfo, ContactInfoDto>();
        CreateMap<CreateContactInfoDto, ContactInfo>();
        CreateMap<UpdateContactInfoDto, ContactInfo>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Doctor Services (offerings) ──────────────────────────────────
        CreateMap<DoctorService, DoctorServiceDto>();
        CreateMap<CreateDoctorServiceDto, DoctorService>();
        CreateMap<UpdateDoctorServiceDto, DoctorService>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── FAQs ─────────────────────────────────────────────────────────
        CreateMap<Faq, FaqDto>();
        CreateMap<CreateFaqDto, Faq>();
        CreateMap<UpdateFaqDto, Faq>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Hero Images ──────────────────────────────────────────────────
        CreateMap<HeroImage, HeroImageDto>();
        CreateMap<CreateHeroImageDto, HeroImage>();
        CreateMap<UpdateHeroImageDto, HeroImage>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Qualifications ───────────────────────────────────────────────
        CreateMap<Qualification, QualificationDto>();
        CreateMap<CreateQualificationDto, Qualification>();
        CreateMap<UpdateQualificationDto, Qualification>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Reviews ──────────────────────────────────────────────────────
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>();
        CreateMap<UpdateReviewDto, Review>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Social Links ─────────────────────────────────────────────────
        CreateMap<SocialLink, SocialLinkDto>();
        CreateMap<CreateSocialLinkDto, SocialLink>();
        CreateMap<UpdateSocialLinkDto, SocialLink>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Stats ────────────────────────────────────────────────────────
        CreateMap<Stat, StatDto>();
        CreateMap<CreateStatDto, Stat>();
        CreateMap<UpdateStatDto, Stat>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ── Technology Highlights ────────────────────────────────────────
        CreateMap<TechnologyHighlight, TechnologyHighlightDto>();
        CreateMap<CreateTechnologyHighlightDto, TechnologyHighlight>();
        CreateMap<UpdateTechnologyHighlightDto, TechnologyHighlight>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}