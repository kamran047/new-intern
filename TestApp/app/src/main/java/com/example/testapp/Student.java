package com.example.testapp;

import android.os.Parcel;
import android.os.Parcelable;

public class Student implements Parcelable {
    int studentId;
    String name;
    String email;
    String password;
    String confirmPassword;
    int phoneNo;

    public Student() {
    }

    protected Student(Parcel in) {
        studentId = in.readInt();
        name = in.readString();
        email = in.readString();
        password = in.readString();
        confirmPassword = in.readString();
        phoneNo = in.readInt();
    }

    public static final Creator<Student> CREATOR = new Creator<Student>() {
        @Override
        public Student createFromParcel(Parcel in) {
            return new Student(in);
        }

        @Override
        public Student[] newArray(int size) {
            return new Student[size];
        }
    };

    public int getStudentId() {
        return studentId;
    }

    public String getName() {
        return name;
    }

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    public String getConfirmPassword() {
        return confirmPassword;
    }

    public int getPhoneNo() {
        return phoneNo;
    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(studentId);
        dest.writeString(name);
        dest.writeString(email);
        dest.writeString(password);
        dest.writeString(confirmPassword);
        dest.writeInt(phoneNo);
    }
}

