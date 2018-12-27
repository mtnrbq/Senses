module Router
    open Fable.Import.Browser
    open Elmish.Browser.UrlParser

    let currentUrl () = window.location.href

    let i64 state =
        custom "i64" (System.Int64.TryParse >> function true, value -> Ok value | _ -> Error "Can't parse int64" ) state

    type Page =
        | Home
        | Sign of string option
        | Datasets
        | DatasetCreate
        | DatasetDetails of int64
        | DatasetTaskCreate of int64
        | DatasetTasks of int64
        | DatasetSliceCreate of int64
        | DatasetResources of int64
        | Tasks
        | Labels
        | LabelCreate

    let pageParser: Parser<Page->Page, Page>  =
        oneOf
            [ map Home top
              map Sign (s "sign" <?> stringParam "redirectUrl")
              map Datasets (s "datasets")
              map DatasetCreate (s "datasets" </> s "create")
              map DatasetDetails (s "datasets" </> i64)
              map DatasetSliceCreate (s "datasets" </> i64 </> s "slices" </> s "create")
              map DatasetResources (s "datasets" </> i64 </> s "slices")
              map DatasetTaskCreate (s "datasets" </> i64 </> s "tasks" </> s "create")
              map DatasetTasks (s "datasets" </> i64 </> s "tasks")
              map Labels (s "labels")
              map LabelCreate (s "labels" </> s "create")
              map Tasks (s "tasks") ]
