package com.example.testapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MainActivity extends AppCompatActivity {
    private TextView _username;
    private TextView _password;
    private Button _login;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _login = findViewById(R.id.btnLogin);

        _login.setOnClickListener(v -> {
            Retrofit retrofit = new Retrofit.Builder()
                    .baseUrl("http://192.168.1.7:45456/")
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
            _username = findViewById(R.id.username);
            _password = findViewById(R.id.password);
            String username = _username.getText().toString();
            String password = _password.getText().toString();
            StudentAPI service = retrofit.create(StudentAPI.class);
            Call<AccessToken> call = service.login("password", username, password);
            call.enqueue(new Callback<AccessToken>() {
                @Override
                public void onResponse(Call<AccessToken> call, Response<AccessToken> response) {
                    if (!response.isSuccessful()) {
                        return;
                    }
                    Toast.makeText(MainActivity.this, "success",
                            Toast.LENGTH_LONG).show();
                    Intent intent = new Intent(MainActivity.this, StudentListing.class);
                    startActivity(intent);
                }

                @Override
                public void onFailure(Call call, Throwable t) {
                    Toast.makeText(MainActivity.this, "Some Problem Occur",
                            Toast.LENGTH_LONG).show();
                }
            });
        });
    }
}