const puppeteer = require('puppeteer')

const main = async () => {
  const browser = await puppeteer.launch({headless:false, slowMo: 20})
  const page = await browser.newPage()
  //Login
  await page.goto('file://E:/Projects/Internship/Add Delete Student/html/login.html')
  await page.type('input#txtUsername',"Kamran");
  await page.type('input#txtPassword',"Kamran");
  await page.click('#btnLogin');
  //Adding Student
  await page.click("#input_feilds");
  await page.type("input#fname","Testing");
  await page.type("input#email","Testing@gmail.com");
  await page.type("input#password","abc");
  await page.type("input#confirm_password","abc");
  await page.type("input#number","0300");
  const inputUploadHandle = await page.$('input[type=file]');
  await inputUploadHandle.uploadFile('E:\\a.png');
  await page.select("#course_data","1","3","5");
  await page.click("#save");
  //Delete Student
  await page.click("#delete0");
  //Edit Student
  await page.waitForSelector('#edit0');
  await page.click("#edit0");
  await page.evaluate( () => document.getElementById("fname").value = "")
  await page.evaluate( () => document.getElementById("email").value = "")
  await page.evaluate( () => document.getElementById("password").value = "")
  await page.evaluate( () => document.getElementById("confirm_password").value = "")
  await page.evaluate( () => document.getElementById("number").value = "")
  await page.type("#fname","Hassan");
  await page.type("#email","Hassan@gmail.com");
  await page.type("#password","abc");
  await page.type("#confirm_password","abc");
  await page.type("#number","0300");
  const inputUploadHandle = await page.$('input[type=file]');
  await inputUploadHandle.uploadFile('E:\\a.png');
  await page.select("#course_data","2","4");
  await page.click("#save");
  //Logout
  await page.click("#btnLogout");
}
main()