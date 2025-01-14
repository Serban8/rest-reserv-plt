import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await fetch("http://localhost:5111/api/Account/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (response.ok) {
        const token = await response.text();
        localStorage.setItem("token", token); // Store token

        // Decode the token to extract user details
        const payload = JSON.parse(atob(token.split(".")[1]));
        console.log("Decoded Token Payload:", payload); // Log payload for debugging

        // Store user details in localStorage
        if (payload.firstName) {
          localStorage.setItem("firstname", payload.firstName);
        } else {
          console.warn("First name is missing in the token payload.");
        }
        if (payload.fullName) {
          localStorage.setItem("name", payload.fullName);
        } else {
          console.warn("Full name is missing in the token payload.");
        }
        if (payload.userId) {
          localStorage.setItem("userId", payload.userId);
        } else {
          console.warn("User ID is missing in the token payload.");
        }

        alert("Login successful!");
        navigate("/");
      } else {
        alert("Invalid credentials!");
      }
    } catch (error) {
      console.error("Error logging in:", error);
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <div>
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button type="submit">Login</button>
      </form>
      <p>
        Don’t have an account? <a href="/register">Register here</a>.
      </p>
    </div>
  );
};

export default LoginPage;
