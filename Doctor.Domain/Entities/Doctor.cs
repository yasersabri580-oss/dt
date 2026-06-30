namespace Doctor.Domain.Entities;

/// <summary>Top-level doctor account record.</summary>
public class Doctor
{
    public Guid Id { get; set; }
    public Dictionary<string, string> Slug { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public long UserId { get; set; }

    // Navigation
    public DoctorProfile? Profile { get; set; }
    public ContactInfo? ContactInfo { get; set; }
    public ICollection<NavLink> NavLinks { get; set; } = new List<NavLink>();
    public ICollection<AboutHighlight> AboutHighlights { get; set; } = new List<AboutHighlight>();
    public ICollection<DoctorService> Services { get; set; } = new List<DoctorService>();
    public ICollection<TechnologyHighlight> TechnologyHighlights { get; set; } = new List<TechnologyHighlight>();
    public ICollection<Stat> Stats { get; set; } = new List<Stat>();
    public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    public ICollection<Faq> Faqs { get; set; } = new List<Faq>();
    public ICollection<AppointmentOption> AppointmentOptions { get; set; } = new List<AppointmentOption>();
    public ICollection<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<HeroImage> HeroImages { get; set; } = new List<HeroImage>();

    public required User User { get; set; }
}
