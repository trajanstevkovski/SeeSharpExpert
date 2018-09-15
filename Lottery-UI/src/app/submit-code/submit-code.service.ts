import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IUserCode, IAward } from "../winners-list/winner-list.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class SubmitCodeService {

    private url: string = "http://localhost:50438/api/Lottery/";

    constructor(private httpClient: HttpClient) {

    }

    submitCode(userCode: IUserCode): Observable<IAward> {
        return this.httpClient.post<IAward>(this.url + "SubmitCode", userCode);
    }
}