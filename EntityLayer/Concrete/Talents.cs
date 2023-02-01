using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Talents
	{
		[Key]
		public int Id { get; set; }
		public string TalentName { get; set; }
		public int Knowledge { get; set; }
	}
}
