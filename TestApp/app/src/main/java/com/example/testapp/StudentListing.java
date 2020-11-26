package com.example.testapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Parcelable;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class StudentListing extends AppCompatActivity {
    private LinearLayout linearLayout;
    private Button _addStudent;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student_listing);

        linearLayout = findViewById(R.id.showDataLayout);
        _addStudent = findViewById(R.id.addStudentBtn);

        _addStudent.setOnClickListener(v -> {
            Intent intent = new Intent(StudentListing.this, AddStudent.class);
            startActivity(intent);
        });

        Retrofit retrofit = RetrofitCall.getClient();

        StudentAPI service = retrofit.create(StudentAPI.class);

        Call<List<StudentModel>> call = service.getStudents();

        call.enqueue(new Callback<List<StudentModel>>() {
            @Override
            public void onResponse(Call<List<StudentModel>> call, Response<List<StudentModel>> response) {
                if (!response.isSuccessful()) {
                    return;
                }
                List<StudentModel> students = response.body();
                for (StudentModel student : students) {
                    String content = "";
                    content += "Student Name: " + student.student.name + "\n";
                    content += "Student Email: " + student.student.email + "\n";
                    content += "Student Password: " + student.student.password + "\n";
                    content += "Student ConfirmPassword: " + student.student.confirmPassword + "\n";
                    content += "Student PhoneNo: " + student.student.phoneNo + "\n";

                    TextView textViewResult = new TextView(StudentListing.this);
                    LinearLayout.LayoutParams lparams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT);
                    textViewResult.setLayoutParams(lparams);
                    textViewResult.setText(content);
                    linearLayout.addView(textViewResult);
                    addButton("Edit", linearLayout, lparams, student.student);
                    addButton("Delete", linearLayout, lparams, student.student);
                }
            }

            @Override
            public void onFailure(Call<List<StudentModel>> call, Throwable t) {
                Toast.makeText(StudentListing.this, "Some Problem Occur",
                        Toast.LENGTH_LONG).show();
            }
        });
    }

    public void delete(int studentId) {
        Retrofit retrofit =RetrofitCall.getClient();
        StudentAPI service = retrofit.create(StudentAPI.class);
        Call<String> call = service.deleteStudent(studentId);
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (!response.isSuccessful()) {
                    return;
                }
                Toast.makeText(StudentListing.this, "Student Deleted Successfully",
                        Toast.LENGTH_LONG).show();
                Intent intent = new Intent(StudentListing.this, StudentListing.class);
                startActivity(intent);
            }

            @Override
            public void onFailure(Call call, Throwable t) {
                Toast.makeText(StudentListing.this, "Some Problem Occur",
                        Toast.LENGTH_LONG).show();
            }
        });
    }

    public void edit(Student student) {
        Intent intent = new Intent(StudentListing.this, AddStudent.class);
        intent.putExtra("Student",student);
        startActivity(intent);

    }

    public void addButton(String dataToDisplay, LinearLayout linearLayout, LinearLayout.LayoutParams lparams, Student student) {
        Button button = new Button(StudentListing.this);
        button.setText(dataToDisplay);
        button.setTextColor(Color.BLACK);

        if (dataToDisplay == "Delete") {
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    delete(student.studentId);
                }
            });
        } else {
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    edit(student);
                }
            });
        }
        linearLayout.addView(button);
    }
}