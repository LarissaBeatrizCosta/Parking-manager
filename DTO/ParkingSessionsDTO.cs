using System.Text.Json;
using System.Text.Json.Serialization;

namespace parking_manager.DTO
{
    public class ParkingSessionsDTO
    {
        [JsonPropertyName("vehicleId")]
        public required string VehiclePlate { get; set; }
        [JsonPropertyName("entryDate")]
        public string EntryDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        [JsonPropertyName("exitDate")]
        public string? ExitDate { get; set; }
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
        [JsonPropertyName("hoursPaid")]
        public double HoursPaid { get; set; }
        [JsonPropertyName("pricePerHour")]
        public double PricePerHour { get; set; }
        [JsonPropertyName("totalPrice")]
        public double TotalPrice { get; set; }

        public static List<ParkingSessionsDTO>? FromJson(string json)
        {
            return JsonSerializer.Deserialize<List<ParkingSessionsDTO>>(json);
        }

    }
}