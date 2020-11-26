package com.example.testapp;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface StudentAPI {
    @GET("student")
    Call<List<StudentModel>> getStudents();

    @GET("course")
    Call<List<Course>> getCourses();

    @POST("Login")
    @FormUrlEncoded
    Call<AccessToken> login(@Field("grant_type") String grant_type,
                            @Field("username") String username,
                            @Field("password") String password);

    @POST("student")
    Call<String> saveStudent(@Body StudentModel studentModel);

    @DELETE("student/{studentId}")
    Call<String> deleteStudent(@Path("studentId") int studentId);

    @PUT("student")
    Call<String> updateStudent(@Body StudentModel studentModel);
}

