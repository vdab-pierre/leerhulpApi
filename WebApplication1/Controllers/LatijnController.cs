using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Models.latijn;

namespace WebApplication1.Controllers
{
    public class LatijnController : ApiController
    {
        private leerhulpEntities _db = new leerhulpEntities();
        [Route("api/latijn/caputs")]
        public IHttpActionResult GetAllCaputs()
        {
            var deCaputs = (from c in _db.caputs
                            orderby c.naam
                            select new CaputDto
                            {
                                id = c.id,
                                naam = c.naam
                            }).ToList();
            return Ok(deCaputs);
        }
        [Route("api/latijn/woorden")]
        public IHttpActionResult GetAllWoorden()
        {
            var deWoorden = (from w in _db.woorden
                                 join c in _db.caputs
                                 on w.caputId equals c.id
                             select new WoordDto
                             {
                                 Id = w.id,
                                 Term = w.term,
                                 Genus = w.genus,
                                 Caput=new CaputDto { id=c.id,naam=c.naam}
                             }).ToList();
            foreach (var w in deWoorden)
            {
                w.AanvInfo = (from ai in _db.aanvullendeInfo
                              where ai.woordId == w.Id
                              select new AanvInfoDto
                              {
                                  Id = ai.id,
                                  Term = ai.term
                              })
                              .ToList();
                w.Vertalingen = (from v in _db.vertalingen
                                 where v.woordId == w.Id
                                 select new VertalingDto
                                 {
                                     Id = v.id,
                                     Term = v.term
                                 })
                                 .ToList();
            }
            return Ok(deWoorden);
        }
        [Route("api/latijn/woorden/caput/{caputId}")]
        public IHttpActionResult GetWoordenVanCaput(int caputId)
        {
            var deWoorden = (from w in _db.woorden
                                join c in _db.caputs
                                on w.caputId equals c.id
                             where c.id == caputId
                             select new WoordDto
                             {
                                 Id = w.id,
                                 Term = w.term,
                                 Genus = w.genus,
                                 Caput = new CaputDto { id = c.id, naam = c.naam }
                             }).ToList();
            foreach (var w in deWoorden)
            {
                w.AanvInfo = (from ai in _db.aanvullendeInfo
                              where ai.id == w.Id
                              select new AanvInfoDto
                              {
                                  Id = ai.id,
                                  Term = ai.term
                              })
                              .ToList();
                w.Vertalingen = (from v in _db.vertalingen
                                 where v.id == w.Id
                                 select new VertalingDto
                                 {
                                     Id = v.id,
                                     Term = v.term
                                 })
                                 .ToList();
            }
            return Ok(deWoorden);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
