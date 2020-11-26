package com.example.testapp;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitCall {
    public static Retrofit getClient() {
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://192.168.1.7:45456/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        return retrofit;
    }
}
