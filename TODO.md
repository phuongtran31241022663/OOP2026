# Implementation TODO

## Task 1: TextBox Placeholders (UcAuth)
- [x] Add placeholder text behavior for txtLoginPhone, txtLoginPassword, txtRegName, txtRegPhone, txtRegPassword
- [x] Handle Enter/Leave events with ForeColor switching
- [x] Handle PasswordChar for password fields when placeholder is active/inactive

## Task 2: Registration Success + Auto-navigation + Try-catch
- [x] Fix UcAuth.OnRegisterClicked: auto-login after registration, show MessageBox, pass actual User to RegisterSucceeded
- [x] Add try-catch to UcDriver: OnArrivedClicked, OnStartTripClicked, OnCompleteTripClicked, OnCancelTripClicked
- [x] Add try-catch to UcPassenger: OnCancelSearchClicked, OnCancelTripClicked, OnConfirmPaymentClicked
- [x] Add try-catch to TripHistoryForm.LoadTrips
- [x] Fix UcAdmin.UpdateStats empty catch

## Task 3: Seed Data
- [x] Create Infrastructure/Data/DataSeeder.cs with arrays
- [x] Seed 50 drivers (16 Cars, 34 Motorbikes), Wallet=50000 VND, Available, password=123456
- [x] Seed 5 sample passengers with password=123456
- [x] Seed 5-10 Completed trips with real HCM coordinates
- [x] Call DataSeeder in AppServiceBundle.CreateDefaultAsync

## Follow-up
- [x] Build verification - PASSED
- [ ] Delete old JSON data files for fresh test

