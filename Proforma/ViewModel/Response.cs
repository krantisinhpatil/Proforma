using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class CategoryResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class CompanyResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
    }

    public class MVPLPCompanyResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<MVPLPCompanyViewModel> Companies { get; set; }
    }

    public class SearchCompanyResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CompanySearchModel> Companies { get; set; }
    }

    public class CompanyDetailsResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public CompanyDetailsViewModel CompanyDetails { get; set; }
    }

    public class FilterCriteriaResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<FilterCriteriaViewModel> Criteria { get; set; }
    }

    public class EmailVerificationResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class RegisterResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public int UserId { get; set; }
    }

    public class LoginResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public int UserId { get; set; }
    }

    public class ForgotPasswordResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public string TemporaryPassword { get; set; }
    }

    public class ResetPasswordResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class AddToFavouritesResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class PushResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<PushNotification> Categories { get; set; }
    }
    

    /************************************************/
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}