using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NUnit.Framework;

namespace OOBootcamp.Test;

public class SmartParkingBoyTest
{
    private const int MAX_CAPACITY = 4;
    private SmartParkingBoy _smartParkingBoy = null!;
    private IEnumerable<ParkingLot> _parkingLots = null!;
    private Fixture _fixture = new Fixture();

    [SetUp]
    public void Setup()
    {
        _parkingLots = new List<ParkingLot>()
        {
            new ParkingLot(MAX_CAPACITY, 5, _fixture.Create<string>()),
            new ParkingLot(MAX_CAPACITY * 2, 5, _fixture.Create<string>()),
        };

        _smartParkingBoy = new SmartParkingBoy(_parkingLots);
    }

    [Test]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_empty_parking_lot()
    {
        string licensePlate = _fixture.Create<string>();
        var vehicle = new Vehicle(licensePlate);

        // park successfully
        Assert.True(_smartParkingBoy.ParkVehicle(vehicle));

        var parkingLot = _smartParkingBoy.GetParkingLot(vehicle);

        // park in second parkinglot
        Assert.NotNull(parkingLot);
        Assert.AreEqual(_parkingLots.Last().Name, parkingLot!.Name);
    }

    [Test]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_available_count_1_less_than_2()
    {
        // Arrange
        _parkingLots.First().ParkVehicle(new Vehicle(_fixture.Create<string>()));

        // Action
        string licensePlate = _fixture.Create<string>();
        var vehicle = new Vehicle(licensePlate);

        // Assertion
        Assert.True(_smartParkingBoy.ParkVehicle(vehicle));

        var parkingLot = _smartParkingBoy.GetParkingLot(vehicle);

        // park in second parkinglot
        Assert.NotNull(parkingLot);
        Assert.AreEqual(_parkingLots.Last().Name, parkingLot!.Name);
    }

    [Test]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_available_count_1_equals_2()
    {
        // Arrange
        // park 1 and 2 has avaiblable 2, 1 is 2/4 = 50%, 2 is 2/8 = 25%
        foreach (var parkingLot in _parkingLots)
        {
            for (int i = 0; i < parkingLot.MaxCapacity - 2; i++)
            {
                parkingLot.ParkVehicle(new Vehicle(_fixture.Create<string>()));
            }
        }

        // Action
        string licensePlate = _fixture.Create<string>();
        var vehicle = new Vehicle(licensePlate);

        // Assertion
        Assert.True(_smartParkingBoy.ParkVehicle(vehicle));

        var actualParkingLot = _smartParkingBoy.GetParkingLot(vehicle);

        // park in second parkinglot
        Assert.NotNull(actualParkingLot);
        Assert.AreEqual(_parkingLots.First().Name, actualParkingLot!.Name);
    }

    [Test]
    public void should_park_vehicle_failure_when_ParkVehicle_given_full()
    {
        // Arrange
        foreach (var parkingLot in _parkingLots)
        {
            for (int i = 0; i < parkingLot.MaxCapacity; i++)
            {
                parkingLot.ParkVehicle(new Vehicle(_fixture.Create<string>()));
            }
        }

        // Action
        // Assertion
        Assert.Throws<NoParkingSlotAvailableException>(() => _smartParkingBoy.ParkVehicle(new Vehicle(_fixture.Create<string>())));
    }
}
