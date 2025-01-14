```mermaid

stateDiagram-v2
    [*] --> Login

    state Login {
        [*] --> LoginPage : NavigateToLogin
        LoginPage --> LoggedIn : LoginSuccess
        LoginPage --> RegistrationPage : NavigateToRegister
        RegistrationPage --> LoggedIn : RegistrationSuccess
        LoginPage --> ErrorState : LoginFailed
    }

    state LoggedIn {
        [*] --> HomePage : NavigateToHome

        HomePage --> ViewingRestaurants : ViewRestaurants
        ViewingRestaurants --> RestaurantDetailsPage : ViewRestaurantDetails
        RestaurantDetailsPage --> ViewingTables : ViewTables
        ViewingTables --> ReservingTable : ReserveTable

        ReservingTable --> ReservationSuccessPage : ReservationConfirmed
        ReservingTable --> ErrorState : ReservationFailed

        HomePage --> FeedbackPage : ViewFeedback
        FeedbackPage --> FeedbackSubmitted : SubmitFeedback
        FeedbackPage --> ErrorState : FeedbackFailed

        HomePage --> UserReservationsPage : ViewUserReservations
        UserReservationsPage --> ProvidingFeedback : ProvideFeedback
        ProvidingFeedback --> FeedbackSubmitted : FeedbackSuccess
        ProvidingFeedback --> ErrorState : FeedbackFailed

        HomePage --> AdminPanel : AdminAccess
    }

    state AdminPanel {
        [*] --> AdminHomePage : AdminNavigateToHome

        AdminHomePage --> ManagingRestaurants : ManageRestaurants
        ManagingRestaurants --> AddRestaurantPage : AddRestaurant
        AddRestaurantPage --> ManagingRestaurants : RestaurantAdded
        ManagingRestaurants --> RemoveRestaurantPage : RemoveRestaurant
        RemoveRestaurantPage --> ManagingRestaurants : RestaurantRemoved

        AdminHomePage --> ManagingTables : ManageTables
        ManagingTables --> AddTablePage : AddTable
        AddTablePage --> ManagingTables : TableAdded
        ManagingTables --> RemoveTablePage : RemoveTable
        RemoveTablePage --> ManagingTables : TableRemoved

        AdminHomePage --> ViewingReservationsPage : ViewAllReservations
        ViewingReservationsPage --> ConfirmingReservation : ConfirmReservation
        ConfirmingReservation --> ViewingReservationsPage : ReservationConfirmed
        ViewingReservationsPage --> CancellingReservation : CancelReservation
        CancellingReservation --> ViewingReservationsPage : ReservationCancelled
    }

    ReservationSuccessPage --> HomePage
    FeedbackSubmitted --> HomePage
    ErrorState --> LoginPage : RetryLogin
    HomePage --> LoginPage : Logout
```
