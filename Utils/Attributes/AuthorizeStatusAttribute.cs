using invoice_system.Utils.Enums;
using Microsoft.AspNetCore.Authorization;

namespace invoice_system.Utils.Attributes;

public class AuthorizeStatusAttribute : AuthorizeAttribute {
    private const string POLICY_PREFIX = "Status";
    public AuthorizeStatusAttribute(Status status = Status.isActive){
        Policy = $"{POLICY_PREFIX}:{status}";
    }
}