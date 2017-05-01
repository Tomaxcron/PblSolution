using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Pbl.Models.DbClasses
{
    public class MProblemaXMed : IFunctions<ProblemaXMed>
    {

        private FamervEntities db;

        public MProblemaXMed()
        {
            db = Singletone.InstanceFamerv;
        }

        public bool Add(ProblemaXMed t)
        {
            try
            {
                db.ProblemaXMed.Add(t);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
            return true;
        }

        public List<ProblemaXMed> Bring(Expression<Func<ProblemaXMed, bool>> predicate)
        {
            return db.ProblemaXMed.Where(predicate).ToList();
        }

        public List<Problema> RetornaProblemasCadastrados(int idMed)
        {
            List<Problema> listProblemas = new List<Problema>();
            List<ProblemaXMed> listProblemaXMed = this.Bring(c => c.idMed == idMed);
            foreach (var problema in listProblemaXMed)
            {
                listProblemas.Add(problema.Problema);
            }
            return listProblemas;
        }

        public List<Problema> RetornaProblemasDisponiveis(int idMed)
        {
             return db.Problema.Except(RetornaProblemasCadastrados(idMed)).ToList();
        }

        public List<ProblemaXMed> BringAll()
        {
            throw new NotImplementedException();
        }

        public ProblemaXMed BringOne(Expression<Func<ProblemaXMed, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ProblemaXMed t)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProblemaXMed t)
        {
            throw new NotImplementedException();
        }
    }
}