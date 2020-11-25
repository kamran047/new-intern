package com.example.testapp;

import androidx.appcompat.app.AppCompatActivity;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Parcel;
import android.provider.MediaStore;
import android.util.Base64;
import android.util.Log;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.io.ByteArrayOutputStream;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class AddStudent extends AppCompatActivity {

    private TextView _name;
    private TextView _email;
    private TextView _password;
    private TextView _confirmPassword;
    private TextView _phoneNo;

    private Button _addStudent;
    private Button _chooseCourses;
    private Button _chooseimage;
    private Button _submit;

    Bitmap imageFile;
    String[] base64Image;

    private ImageView imageView;
    private List<String> courseIdList;
    Student student = new Student();
    StudentModel model = new StudentModel();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_student);
        Student studentData = getIntent().getParcelableExtra("Student");
        _name = findViewById(R.id.name);
        _email = findViewById(R.id.email);
        _password = findViewById(R.id.password);
        _confirmPassword = findViewById(R.id.confirmPassword);
        _phoneNo = findViewById(R.id.phoneNo);


        Retrofit retrofit = RetrofitCall.getClient();
        StudentAPI service = retrofit.create(StudentAPI.class);

        if (studentData != null) {
            _name.setText(studentData.name);
            _email.setText(studentData.email);
            _password.setText(studentData.password);
            _confirmPassword.setText(studentData.confirmPassword);
            _phoneNo.setText(Integer.toString(studentData.phoneNo));
        }

        _chooseCourses = findViewById(R.id.chooseCoursesBtn);
        _chooseCourses.setOnClickListener(v -> {
            Call<List<Course>> call = service.getCourses();
            call.enqueue(new Callback<List<Course>>() {
                             @Override
                             public void onResponse(Call<List<Course>> call, Response<List<Course>> response) {
                                 if (!response.isSuccessful()) {
                                     return;
                                 }
                                 List<Course> courses = response.body();
                                 selectItemsDialog(courses);
                             }

                             @Override
                             public void onFailure(Call<List<Course>> call, Throwable t) {
                             }
                         }
            );
        });

        imageView = findViewById(R.id.imageView);
        _chooseimage = findViewById(R.id.uploadImageBtn);
        _chooseimage.setOnClickListener(v -> {
            selectImage(this);
        });

        _submit = findViewById(R.id.submitBtn);
        _submit.setOnClickListener(v -> {
            student.name = _name.getText().toString();
            student.email = _email.getText().toString();
            student.password = _password.getText().toString();
            student.confirmPassword = _confirmPassword.getText().toString();
            student.phoneNo = Integer.parseInt(_phoneNo.getText().toString());

            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            imageFile.compress(Bitmap.CompressFormat.PNG, 100, baos);
            byte[] imageBytes = baos.toByteArray();
            String imageString = Base64.encodeToString(imageBytes, Base64.DEFAULT);
            base64Image = imageString.split(",", 2);

            model.courses = courseIdList;
            model.imagePath = base64Image;

            Call<String> call;
            if (studentData == null) {
                model.student = student;
                call = service.saveStudent(model);
            } else {
                studentData.name = student.name;
                studentData.email = student.email;
                studentData.password = student.password;
                studentData.confirmPassword = student.password;
                studentData.phoneNo = student.phoneNo;
                model.student = studentData;
                call = service.updateStudent(model);
            }

            call.enqueue(new Callback<String>() {
                             @Override
                             public void onResponse(Call<String> call, Response<String> response) {
                                 if (!response.isSuccessful()) {
                                     return;
                                 }
                                 if (studentData!=null) {
                                     studentData.studentId = 0;
                                 }
                                 Toast.makeText(AddStudent.this, "Data Updated Successfully",
                                         Toast.LENGTH_LONG).show();
                                 Intent intent = new Intent(AddStudent.this, StudentListing.class);
                                 startActivity(intent);
                             }

                             @Override
                             public void onFailure(Call<String> call, Throwable t) {
                                 Toast.makeText(AddStudent.this, "Some Problem Occur",
                                         Toast.LENGTH_LONG).show();
                             }
                         }
            );
        });
    }


    private void selectImage(Context context) {
        final CharSequence[] options = {"Take Photo", "Choose from Gallery", "Cancel"};

        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setTitle("Choose your profile picture");

        builder.setItems(options, new DialogInterface.OnClickListener() {

            @Override
            public void onClick(DialogInterface dialog, int item) {

                if (options[item].equals("Take Photo")) {
                    Intent takePicture = new Intent(android.provider.MediaStore.ACTION_IMAGE_CAPTURE);
                    startActivityForResult(takePicture, 0);

                } else if (options[item].equals("Choose from Gallery")) {
                    Intent pickPhoto = new Intent(Intent.ACTION_PICK, android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
                    startActivityForResult(pickPhoto, 1);

                } else if (options[item].equals("Cancel")) {
                    dialog.dismiss();
                }
            }
        });
        builder.show();
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode != RESULT_CANCELED) {
            switch (requestCode) {
                case 0:
                    if (resultCode == RESULT_OK && data != null) {
                        Bitmap selectedImage = (Bitmap) data.getExtras().get("data");
                        imageFile = selectedImage;
                        imageView.setImageBitmap(selectedImage);
                    }

                    break;
                case 1:
                    if (resultCode == RESULT_OK && data != null) {
                        Uri selectedImage = data.getData();
                        String[] filePathColumn = {MediaStore.Images.Media.DATA};
                        if (selectedImage != null) {
                            Cursor cursor = getContentResolver().query(selectedImage,
                                    filePathColumn, null, null, null);
                            if (cursor != null) {
                                cursor.moveToFirst();

                                int columnIndex = cursor.getColumnIndex(filePathColumn[0]);
                                String picturePath = cursor.getString(columnIndex);
                                imageView.setImageBitmap(BitmapFactory.decodeFile(picturePath));
                                cursor.close();
                            }
                        }

                    }
                    break;
            }
        }
    }


    public void selectItemsDialog(List<Course> courses) {
        int[] courseId = new int[courses.size()];
        String[] courseName = new String[courses.size()];
        for (int i = 0; i < courses.size(); i++) {
            courseId[i] = courses.get(i).courseId;
            courseName[i] = courses.get(i).courseName;
        }
        List<String> mSelectedItems = new ArrayList();
        AlertDialog.Builder builder = new AlertDialog.Builder(AddStudent.this);
        builder.setTitle("Choose Subjects");
        builder.setMultiChoiceItems(courseName, null,
                (dialog, which, isChecked) -> {
                    if (isChecked) {
                        mSelectedItems.add(Integer.toString(courseId[which]));
                    } else if (mSelectedItems.contains(which)) {
                        mSelectedItems.remove(Integer.valueOf(which));
                    }
                })
                .setPositiveButton("OK", (dialog, id) -> {
                    courseIdList = mSelectedItems;
                })
                .setNegativeButton("cancel", (dialog, id) -> dialog.cancel()).show();
    }
}