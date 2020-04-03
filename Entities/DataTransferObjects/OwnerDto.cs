using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public sealed class OwnerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}
