from pathlib import Path
import re

root = Path("OOP2026")

replacements = [
    ("IDriverWalletService", "IWalletSvc"),
    ("IDriverCommandService", "IDrvCmd"),
    ("IDriverQueryService", "IDrvQry"),
    ("ITripCommandService", "ITripCmd"),
    ("ITripQueryService", "ITripQry"),
    ("IUserRepository", "IUsrRepo"),
    ("ITripRepository", "ITripRepo"),
    ("IVehicleRepository", "IVehRepo"),
    ("IPolicyRepository", "IPolRepo"),
    ("IReviewRepository", "IRevRepo"),
    ("IUserService", "IUsrSvc"),
    ("IMatchingService", "IMatchSvc"),
    ("IFareService", "IFareSvc"),
    ("IMapService", "IMapSvc"),
    ("IPassengerService", "IPsgSvc"),
    ("IAdminService", "IAdmSvc"),
    ("IReviewService", "IRevSvc"),
    ("DriverWalletService", "WalletSvc"),
    ("DriverCommandService", "DrvCmd"),
    ("DriverQueryService", "DrvQry"),
    ("TripCommandService", "TripCmd"),
    ("TripQueryService", "TripQry"),
    ("MatchingService", "MatchSvc"),
    ("UserService", "UsrSvc"),
    ("FareService", "FareSvc"),
    ("MapService", "MapSvc"),
    ("PassengerService", "PsgSvc"),
    ("AdminService", "AdmSvc"),
    ("ReviewService", "RevSvc"),
    ("UserRepository", "UsrRepo"),
    ("TripRepository", "TripRepo"),
    ("VehicleRepository", "VehRepo"),
    ("PolicyRepository", "PolRepo"),
    ("ReviewRepository", "RevRepo"),
    ("LicenseNumber", "LicNo"),
    ("VehicleId", "VehId"),
    ("TotalTrips", "TotTrip"),
    ("AverageRating", "AvgRat"),
    ("PlateNumber", "Plate"),
    ("BaseFare", "Base"),
    ("PricePerKm", "PriceKm"),
    ("CommissionRate", "CommRate"),
    ("Password", "Pwd"),
    ("Motorbike", "Moto"),
    ("Passenger", "Psg"),
    ("Coordinate", "Coord"),
    ("Location", "Loc"),
    ("Address", "Addr"),
    ("Vehicle", "Veh"),
    ("Driver", "Drv"),
    ("Policy", "Pol"),
    ("Review", "Rev"),
    ("Admin", "Adm"),
    ("User", "Usr"),
]

changed = []
for path in root.rglob("*.cs"):
    text = path.read_text(encoding="utf-8-sig")
    new_text = text
    for old, new in replacements:
        pattern = r"(?<![A-Za-z0-9_])" + re.escape(old) + r"(?![A-Za-z0-9_])"
        new_text = re.sub(pattern, new, new_text)
    if new_text != text:
        path.write_text(new_text, encoding="utf-8-sig")
        changed.append(str(path))

print("\n".join(changed))