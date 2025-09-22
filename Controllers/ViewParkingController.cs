using Microsoft.AspNetCore.Mvc;
using parking_manager.DTO;

namespace parking_manager.Controllers
{
    [Route("[controller]")]
    public class ViewParkingController : Controller
    {
        private readonly string apiBaseUrl = "http://localhost:5291";
        private readonly HttpClient _httpClient;

        public ViewParkingController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            };
        }

        /// Add a new parking session

        [HttpPost("{plate}")]
        public async Task<IActionResult> AddParkingSession(string plate)
        {
            try
            {

                if (plate == null)
                {
                    return BadRequest("A Placa é obrigatória. Tente novamente.");
                }

                HttpResponseMessage response = await _httpClient.PostAsync($"/api/ParkingSessions/{plate}", null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { isOk = true, message = "Sessão adicionada com sucesso!" });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { isOk = false, message = errorContent });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// Finalize a parking session


        [HttpPut("{plate}/finalize")]
        public async Task<IActionResult> FinalizeParkingSession(string plate)
        {
            try
            {
                if (plate == null)
                {
                    return BadRequest("A Placa é obrigatória. Tente novamente.");
                }

                HttpResponseMessage response = await _httpClient.PutAsync($"/api/ParkingSessions/{plate}/finalize", null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { isOk = true, message = "Sessão finalizada com sucesso!" });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { isOk = false, message = errorContent });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        /// Returns the index view with the list of parking sessions


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ParkingSessionsDTO>? sessions = null;

            HttpResponseMessage response = await _httpClient.GetAsync("/api/ParkingSessions");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                sessions = ParkingSessionsDTO.FromJson(content);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao buscar sessões de estacionamento");
            }

            return View(sessions);
        }
    }
}