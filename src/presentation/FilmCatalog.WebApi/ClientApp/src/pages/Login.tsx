import {
  TextInput,
  PasswordInput,
  Checkbox,
  Anchor,
  Paper,
  Title,
  Text,
  Container,
  Group,
  Button,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { Link } from "react-router-dom";
import { login } from "../services/userService";
import { extractErrors } from "../helpers/form";
import { useLogin } from "../api/identity";

export default function Login() {
  const form = useForm({
    initialValues: {
      email: "",
      password: "",
    },

    validate: {
      email: (val: string) => (/^\S+@\S+$/.test(val) ? null : "Invalid email"),
      password: (value) =>
        value.trim() !== "" ? null : "Password is required",
    },
  });

  const mutationLogin = useLogin();

  const onLogin = async ({ email, password }: typeof form.values) => {
    try {
      const { data } = await mutationLogin.mutateAsync({
        email,
        password,
      });

      login({
        id: data.id,
        token: data.token,
        email: data.email,
        username: data.username,
        role: data.role,
      });
    } catch (e) {
      form.setErrors(extractErrors(e));
    }
  };

  return (
    <Container size={420} my={40}>
      <Title
        align="center"
        sx={(theme) => ({
          fontFamily: `Greycliff CF, ${theme.fontFamily}`,
          fontWeight: 900,
        })}
      >
        Welcome back!
      </Title>
      <Text color="dimmed" size="sm" align="center" mt={5}>
        Do not have an account yet?{" "}
        <Link to="/register">
          <Anchor size="sm" component="button">
            Create account
          </Anchor>
        </Link>
      </Text>

      <form onSubmit={form.onSubmit(onLogin)}>
        <Paper withBorder shadow="md" p={30} mt={30} radius="md">
          <TextInput
            label="Email"
            placeholder="email@example.com"
            required
            value={form.values.email}
            onChange={(event) =>
              form.setFieldValue("email", event.currentTarget.value)
            }
            error={form.errors.email && "Invalid email"}
          />
          <PasswordInput
            label="Password"
            placeholder="Your password"
            required
            value={form.values.password}
            onChange={(event) =>
              form.setFieldValue("password", event.currentTarget.value)
            }
            error={
              form.errors.password &&
              "Password should include at least 6 characters"
            }
            mt="md"
          />
          <Group position="apart" mt="lg">
            <Checkbox label="Remember me" />
            <Anchor component="button" size="sm">
              Forgot password?
            </Anchor>
          </Group>
          <Button type="submit" fullWidth mt="xl" className="bg-blue-500">
            Sign in
          </Button>
        </Paper>
      </form>
    </Container>
  );
}
