namespace Lib

module Sorting = 
    let merge a b = 
        let rec innerMerge (l1: int list) (l2: int list) (acc: int list) = 
            if List.isEmpty l1 then
                acc @ l2
            elif List.isEmpty l2 then
                acc @ l1
            else
                let h1 = List.head l1
                let h2 = List.head l2
                if h1 < h2 then 
                    innerMerge (List.tail l1) l2 (acc @ [h1])
                else 
                    innerMerge l1 (List.tail l2) (acc @ [h2])
        innerMerge a b []

    let split (lis: int list) = 
        let rec splitUtil n (lis: int list) (acc: int list) = 
            if n = 0 then (lis,acc)
            else
                splitUtil (n - 1) (List.tail lis) (merge acc [List.head lis])
        let (a,b) = splitUtil ((List.length lis)/2 |> int) lis []
        (b,a)

    let rec sort list = 
        if List.length list = 1 then [list.Head]
        else
            let a,b = split(list) 
            merge (sort a) (sort b)