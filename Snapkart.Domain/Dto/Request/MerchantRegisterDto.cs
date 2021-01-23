using System.Collections.Generic;

namespace Snapkart.Domain.Dto.Request
{
    public class MerchantRegisterDto : CustomerRegisterDto
    {
        public List<int> SubscriptionIds { get; set; }
    }
}