using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NUnit.Framework;

namespace OOBootcamp.Test;

public class GraduateParkingBoyTest
{
    private const int MAX_CAPACITY = 3;
    private GraduateParkingBoy _graduateParkingBoy = null!;
    private IEnumerable<ParkingLot> _parkingLots = null!;
    private Fixture _fixture = new Fixture();

    [SetUp]
    public void Setup()
    {
        _parkingLots = new List<ParkingLot>()
        {
            new ParkingLot(MAX_CAPACITY, 5, _fixture.Create<string>()),
            new ParkingLot(MAX_CAPACITY, 5, _fixture.Create<string>()),
        };

        _graduateParkingBoy = new GraduateParkingBoy(_parkingLots);
    }

    [Test]
    [Category(nameof(DefaultParkingLotStrategy))]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_parking_lot_has_available_space()
    {
        string licensePlate = _fixture.Create<string>();
        Assert.True(_graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(DefaultParkingLotStrategy))]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_parking_lot_1_full()
    {
        string licensePlate = string.Empty;

        for (int i = 0; i < MAX_CAPACITY; i++)
        {
            licensePlate = _fixture.Create<string>();
            _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate));
        }

        licensePlate = _fixture.Create<string>();
        Assert.True(_graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(DefaultParkingLotStrategy))]
    public void should_park_vehicle_failure_when_ParkVehicle_given_parking_lot_has_no_space()
    {
        string licensePlate = string.Empty;

        for (int i = 0; i < MAX_CAPACITY * _parkingLots.Count(); i++)
        {
            licensePlate = _fixture.Create<string>();
            _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate));
        }

        licensePlate = _fixture.Create<string>();
        Assert.Throws<NoParkingSlotAvailableException>(() => _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(DefaultParkingLotStrategy))]
    public void should_retrieve_vehicle_with_correct_fee_when_ParkVehicle_given_vehicle_is_found()
    {
        string licensePlate = _fixture.Create<string>();
        Vehicle vehicle = new Vehicle(licensePlate);

        _graduateParkingBoy.ParkVehicle(vehicle);

        Assert.True(_graduateParkingBoy.RetrieveVehicle(vehicle));
    }

    [Test]
    [Category(nameof(DefaultParkingLotStrategy))]
    public void should_not_retrieve_vehicle_when_ParkVehicle_given_vehicle_is_not_found()
    {
        Assert.Throws<NoParkingSlotRecordException>(() => _graduateParkingBoy.RetrieveVehicle(new Vehicle(_fixture.Create<string>())));
    }

    [Test]
    [Category(nameof(BalanceParkingLotStrategy))]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_parking_lot_has_available_space_by_balance_strategy()
    {
        _graduateParkingBoy.SetStrategy(new BalanceParkingLotStrategy());

        string licensePlate = _fixture.Create<string>();
        Assert.True(_graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(BalanceParkingLotStrategy))]
    public void should_park_vehicle_successfully_when_ParkVehicle_given_parking_lot_1_full_by_balance_strategy()
    {
        _graduateParkingBoy.SetStrategy(new BalanceParkingLotStrategy());

        string licensePlate = string.Empty;

        for (int i = 0; i < MAX_CAPACITY; i++)
        {
            licensePlate = _fixture.Create<string>();
            _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate));
        }

        licensePlate = _fixture.Create<string>();
        Assert.True(_graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(BalanceParkingLotStrategy))]
    public void should_park_vehicle_failure_when_ParkVehicle_given_parking_lot_has_no_space_by_balance_strategy()
    {
        _graduateParkingBoy.SetStrategy(new BalanceParkingLotStrategy());

        string licensePlate = string.Empty;

        for (int i = 0; i < MAX_CAPACITY * _parkingLots.Count(); i++)
        {
            licensePlate = _fixture.Create<string>();
            _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate));
        }

        licensePlate = _fixture.Create<string>();
        Assert.Throws<NoParkingSlotAvailableException>(() => _graduateParkingBoy.ParkVehicle(new Vehicle(licensePlate)));
    }

    [Test]
    [Category(nameof(BalanceParkingLotStrategy))]
    public void should_retrieve_vehicle_with_correct_fee_when_ParkVehicle_given_vehicle_is_found_by_balance_strategy()
    {
        _graduateParkingBoy.SetStrategy(new BalanceParkingLotStrategy());

        string licensePlate = _fixture.Create<string>();
        Vehicle vehicle = new Vehicle(licensePlate);

        _graduateParkingBoy.ParkVehicle(vehicle);

        Assert.True(_graduateParkingBoy.RetrieveVehicle(vehicle));
    }

    [Test]
    [Category(nameof(BalanceParkingLotStrategy))]
    public void should_not_retrieve_vehicle_when_ParkVehicle_given_vehicle_is_not_found_by_balance_strategy()
    {
        _graduateParkingBoy.SetStrategy(new BalanceParkingLotStrategy());

        Assert.Throws<NoParkingSlotRecordException>(() => _graduateParkingBoy.RetrieveVehicle(new Vehicle(_fixture.Create<string>())));
    }
}