using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface ITalentsService
	{
		List<Talents> GetList();

		void TalentsAdd(Talents talents);

		Talents GetByID(int id);

		void TalentsDelete(Talents talents);

		void TalentsUpdate(Talents talents);
	}
}
