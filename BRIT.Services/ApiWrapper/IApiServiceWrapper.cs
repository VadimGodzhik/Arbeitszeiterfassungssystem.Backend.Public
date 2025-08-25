using BRIT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace BRIT.Services.ApiWrapper
{
    public interface IApiServiceWrapper
    {
        //Get (http = Get)
        Task<IList<Angestellte>> GetAllAngestellteAsync();
        Task<Angestellte> GetAngestellteAsync(int angestellteId);
        


        Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenAsync();
        Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByAngestellteIdAsync(int angestellteId);
        Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByDateSpanAsync(DateTime startDate, DateTime endDate);
        Task<IList<Arbeitszeiterfassung>> GetArbeitszeiterfassungenByAngestellteIdWithDateSpanAsync(int angestellteId, DateTime startDate, DateTime endDate);
        Task<Arbeitszeiterfassung> GetArbeitszeiterfassungAsync(int arbeitszeiterfassungId);


        Task<IList<Arbeitsandauer>> GetAllArbeitsandauerAsync();
        Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByAngestellteIdAsync(int angestellteId);
        Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByDateSpanAsync(DateTime startDate, DateTime endDate);
        Task<IList<Arbeitsandauer>> GetAllArbeitsandauerByAngestellteIdWithDateSpanAsync(int angestellteId, DateTime startDate, DateTime endDate);
        Task<Arbeitsandauer> GetArbeitsandauerAsync(int arbeitszeiterfassungId);



        //Add (http = Post)
        Task<Angestellte> AddAngestellteAsync(Angestellte entity);
        Task<Stadt> AddStadtAsync(Stadt entity);
        Task<Hausanschrift> AddHausanschriftAsync(Hausanschrift entity);
        Task<Rolle> AddRolleAsync(Rolle entity);
        Task<Arbeitsort> AddArbeitsortAsync(Arbeitsort entity);
        Task<Arbeitsort> AddKennworttAsync(Kennwort entity);
        Task<Fundort> AddFundortAsync(Fundort entity);
        Task<Arbeitszeiterfassung> AddArbeitszeiterfassungAsync(Arbeitszeiterfassung entity);
        Task<Arbeitsandauer> AddArbeitsandauerAsync(Arbeitsandauer entity);


        //Update (http = Put)
        Task<Angestellte> UpdateAngestellteAsync(int id, Angestellte entity);
        Task<Stadt> UpdateStadtAsync(int id, Stadt entity);
        Task<Hausanschrift> UpdateHausanschriftAsync(int id, Hausanschrift entity);
        Task<Rolle> UpdateRolleAsync(int id, Rolle entity);
        Task<Arbeitsort> UpdateArbeitsortAsync(int id, Arbeitsort entity);
        Task<Kennwort> UpdateKennworttAsync(int id, Kennwort entity);
        Task<Fundort> UpdateFundortAsync(int id, Fundort entity);
        Task<Arbeitszeiterfassung> UpdateArbeitszeiterfassungAsync(int id, Arbeitszeiterfassung entity);
        Task<Arbeitsandauer> UpdateArbeitsandauerAsync(int id, Arbeitsandauer entity);
        

        //Delete (http = Delete)
        Task DeleteAngestellteAsync(int id, Angestellte entity);
        Task DeleteStadtAsync(int id, Stadt entity);
        Task DeleteHausanschriftAsync(int id, Hausanschrift entity);
        Task DeleteRolleAsync(int id, Rolle entity);
        Task DeleteArbeitsortAsync(int id, Arbeitsort entity);
        Task DeleteKennworttAsync(int id, Kennwort entity);
        Task DeleteFundortAsync(int id, Fundort entity);
        Task DeleteArbeitszeiterfassungAsync(int id, Arbeitszeiterfassung entity);
        Task DeleteArbeitsandauerAsync(int id, Arbeitsandauer entity);

    }
}
