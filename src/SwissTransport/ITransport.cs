namespace SwissTransport
{
    public interface ITransport
    {
        Stations GetStations(string query);
        StationBoardRoot GetStationBoard(string station);
        Connections GetConnections(string fromStation, string toStation, string Date, string Time, int IsArrivalTime);
    }
}
