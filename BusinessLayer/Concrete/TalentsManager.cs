using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class TalentsManager : ITalentsService
	{
		ITalentsDal _talentsDal;

		public TalentsManager(ITalentsDal talentsDal)
		{
			_talentsDal = talentsDal;
		}

		public Talents GetByID(int id)
		{
			return _talentsDal.Get(x => x.Id == id);
		}

		public List<Talents> GetList()
		{
			return _talentsDal.List();
		}

		public void TalentsAdd(Talents talents)
		{
			_talentsDal.Insert(talents);
		}

		public void TalentsDelete(Talents talents)
		{
			_talentsDal.Delete(talents);
		}

		public void TalentsUpdate(Talents talents)
		{
			_talentsDal.Update(talents);
		}
	}
}
