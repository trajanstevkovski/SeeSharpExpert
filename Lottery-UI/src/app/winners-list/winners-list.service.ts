import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { IUserCodeAward } from "./winner-list.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class WinnersListService{
    private url: string = "http://localhost:50438/api/Lottery/";

    constructor(private http: HttpClient){}

    getAllWinners() : Observable<Array<IUserCodeAward>>{
        return this.http.get<Array<IUserCodeAward>>(this.url + "AllWinners");
    }
}