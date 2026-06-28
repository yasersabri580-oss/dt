## Table `doctors`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `uuid` | Primary |
| `slug` | `text` |  Unique |
| `is_active` | `bool` |  |
| `created_at` | `timestamptz` |  |

## Table `doctor_profile`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  Unique |
| `experience_years` | `int2` |  |
| `og_locale` | `text` |  Nullable |
| `updated_at` | `timestamptz` |  |
| `created_at` | `timestamp` |  Nullable |
| `full_name` | `jsonb` |  |
| `brand_subline` | `jsonb` |  |
| `tagline` | `jsonb` |  |
| `hero_title` | `jsonb` |  |
| `hero_copy` | `jsonb` |  |
| `primary_cta` | `jsonb` |  |
| `secondary_cta` | `jsonb` |  |
| `mission` | `jsonb` |  |
| `about_paragraph` | `jsonb` |  |
| `schedule` | `jsonb` |  |
| `footer_copy` | `jsonb` |  |
| `seo_title` | `jsonb` |  |
| `seo_title_template` | `jsonb` |  |
| `seo_description` | `jsonb` |  |
| `seo_keywords` | `jsonb` |  |
| `og_title` | `jsonb` |  |
| `og_description` | `jsonb` |  |

## Table `nav_links`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `href` | `text` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `label` | `jsonb` |  |

## Table `about_highlights`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `title` | `jsonb` |  |
| `body` | `jsonb` |  |

## Table `services`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `icon_name` | `text` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `title` | `jsonb` |  |
| `body` | `jsonb` |  |

## Table `technology_highlights`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `title` | `jsonb` |  |
| `body` | `jsonb` |  |

## Table `stats`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `value` | `int4` |  |
| `suffix` | `text` |  Nullable |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `label` | `jsonb` |  |
| `note` | `jsonb` |  |

## Table `qualifications`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `year_label` | `text` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `title` | `jsonb` |  |
| `body` | `jsonb` |  |

## Table `achievements`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `text` | `jsonb` |  |

## Table `faqs`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `question` | `jsonb` |  |
| `answer` | `jsonb` |  |

## Table `appointment_options`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `icon_name` | `text` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `title` | `jsonb` |  |
| `body` | `jsonb` |  |

## Table `contact_info`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  Unique |
| `phone_display` | `text` |  |
| `phone_link` | `text` |  |
| `whatsapp_url` | `text` |  |
| `updated_at` | `timestamptz` |  |
| `address` | `jsonb` |  |

## Table `social_links`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `label` | `text` |  |
| `href` | `text` |  |
| `icon_name` | `text` |  Nullable |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |

## Table `articles`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `slug` | `text` |  |
| `reading_minutes` | `int2` |  |
| `published_at` | `date` |  |
| `cover_url` | `text` |  Nullable |
| `is_published` | `bool` |  |
| `is_featured` | `bool` |  |
| `created_at` | `timestamptz` |  |
| `updated_at` | `timestamptz` |  |
| `title` | `jsonb` |  |
| `excerpt` | `jsonb` |  |
| `category` | `jsonb` |  |
| `content` | `jsonb` |  |

## Table `reviews`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `patient_name` | `text` |  |
| `role` | `text` |  |
| `quote` | `text` |  |
| `rating` | `int2` |  |
| `is_active` | `bool` |  |
| `sort_order` | `int2` |  |
| `created_at` | `timestamptz` |  |

## Table `hero_images`

### Columns

| Name | Type | Constraints |
|------|------|-------------|
| `id` | `int8` | Primary |
| `doctor_id` | `uuid` |  |
| `key_name` | `text` |  |
| `storage_url` | `text` |  |
| `sort_order` | `int2` |  |
| `is_active` | `bool` |  |
| `alt_text` | `jsonb` |  |

